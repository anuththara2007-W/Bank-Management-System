# === CONFIG ===
$RepoPath = "C:\Users\User\Bank-Management-System"
$Branch = "master"

while ($true) {
    Set-Location $RepoPath

    # Check for changes
    $status = git status --porcelain
    if ($status) {
        try {
            git add .
            $commitMessage = "Project Updated"
            git commit -m $commitMessage

            # Try to pull & rebase before pushing
            git pull origin $Branch --rebase

            git push origin $Branch
            Write-Host "✅ Changes pushed at $(Get-Date -Format 'HH:mm:ss')"
        }
        catch {
            Write-Host "⚠️ Error occurred: $($_.Exception.Message)"
        }
    } else {
        Write-Host "No changes detected at $(Get-Date -Format 'HH:mm:ss')"
    }

    # Wait 4 seconds before checking again
    Start-Sleep -Seconds 4
}
