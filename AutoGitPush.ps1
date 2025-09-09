# AutoGitPush.ps1
# Runs in infinite loop: commits & pushes changes every 2 seconds

Set-Location "C:\Users\User\Bank  Management System"

while ($true) {
    # Remove git lock file if it exists
    if (Test-Path ".git\index.lock") {
        Remove-Item ".git\index.lock" -Force
    }

    # Stage changes
    git add .

    # Commit with fixed message
    git commit -m "project updated" --allow-empty

    # Try to rebase pull before pushing
    try {
        git pull origin master --rebase
    } catch {
        Write-Output "[$(Get-Date -Format 'HH:mm:ss')] Pull failed (maybe conflict) - skipping"
    }

    # Push changes
    try {
        git push origin master
        Write-Output "[$(Get-Date -Format 'HH:mm:ss')] Changes committed & pushed"
    } catch {
        Write-Output "[$(Get-Date -Format 'HH:mm:ss')] Push failed - will retry"
    }

    # Wait 2 seconds
    Start-Sleep -Seconds 2
}
