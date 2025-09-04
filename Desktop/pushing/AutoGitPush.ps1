# Set repo path (change if needed)
$RepoPath = "C:\Users\User\source\repos\Bank Management System\Bank Management System\"
Set-Location $RepoPath

while ($true) {
    # Check for changes
    $changes = git status --porcelain
    if ($changes) {
        git add .
        git commit -m "Project Updated"

        try {
            git pull origin main --rebase   # use 'master' if that's your branch
            git push origin main
            Write-Host "✅ Changes pushed"
        }
        catch {
            Write-Host "❌ Push failed: $($_.Exception.Message)"
        }
    }
    else {
        Write-Host "⏳ No changes detected at $(Get-Date)"
    }

    # Wait 2 seconds before checking again
    Start-Sleep -Seconds 2
}
