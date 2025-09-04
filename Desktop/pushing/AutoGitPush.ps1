# === CONFIG ===
$RepoPath = "C:\Users\User\source\repos\Bank Management System\Bank Management System"
$Branch   = "master"   # change if your repo uses "main"

# Go to repo path
Set-Location $RepoPath
Write-Host "🚀 Watching for changes in $RepoPath..."

# Infinite loop
while ($true) {
    try {
        # Check if there are any changes
        if (git status --porcelain) {
            # Stage changes
            git add .

            # Commit with timestamp
            git commit -m "Project Updated"

            # Push to GitHub
            git push origin $Branch

            Write-Host "✅ Project pushed "
        }
        else {
            Write-Host "⏸ No changes at $(Get-Date -Format 'HH:mm:ss')"
        }
    }
    catch {
        Write-Host "⚠️ Error during update: $_"
    }

    # Wait before checking again
    Start-Sleep -Seconds 
}
