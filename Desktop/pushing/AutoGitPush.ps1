# Set repo path (change this to your actual repo folder)
$RepoPath = "C:\Users\User\Desktop\Bank-Management-System"
Set-Location $RepoPath

while ($true) {
    git add .
    git commit -m "Project Updated" --allow-empty

    try {
        git pull origin master --rebase
        git push origin master
        Write-Host "✅ Project Updated"
    }
    catch {
        Write-Host "❌ Push failed: $($_.Exception.Message)"
    }

    # Wait 2 seconds before running again
    Start-Sleep -Seconds 2
}
