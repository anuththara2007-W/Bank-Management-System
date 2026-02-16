# AutoGitPush_Heritage.ps1
# -------------------------
# Auto push script for Heritage web dev repo
# -------------------------

# CHANGE THIS to your local repo path
$RepoPath = "C:\Users\User\source\repos\Heritage"

# Branch to push
$Branch = "master"

Write-Host "🔄 Auto Git Push Script Started..."
Write-Host "Monitoring repo: $RepoPath"

while ($true) {
    try {
        # Step 1: Go to repo
        Set-Location $RepoPath

        # Step 2: Optional: Ensure .gitignore is not blocking project
        $gitignorePath = Join-Path $RepoPath ".gitignore"
        if (Test-Path $gitignorePath) {
            $content = Get-Content $gitignorePath
            # Remove old ignore rules if they accidentally block files
            $blockedFolders = @("node_modules/", "dist/")  # add more if needed
            foreach ($folder in $blockedFolders) {
                if ($content -match [regex]::Escape($folder)) {
                    Write-Host "⚠️  Found wrong ignore rule ($folder) → Fixing..."
                    $fixed = $content | Where-Object {$_ -notmatch [regex]::Escape($folder)}
                    Set-Content $gitignorePath $fixed
                    git rm -r --cached .
                    git add .
                    git commit -m "Fix: removed wrong ignore rule $folder"
                    git push origin $Branch
                }
            }
        }

        # Step 3: Stage, commit, and push changes
        git add .
        git commit -m "Project Updated" 2>$null

        # Pull latest changes before pushing
        git pull origin $Branch --rebase
        git push origin $Branch

        Write-Host "✅ Changes pushed at $(Get-Date -Format "HH:mm:ss")"
    }
    catch {
        Write-Host "❌ Error: $($_.Exception.Message)"
    }

    # Step 4: Wait 2 seconds before checking again
    Start-Sleep -Seconds 2
}
