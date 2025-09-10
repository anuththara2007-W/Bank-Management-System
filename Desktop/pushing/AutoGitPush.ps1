# === CONFIG ===
$RepoPath = "C:\Users\User\Bank-Management-System"
$Branch = "master"

while ($true) {
    Set-Location $RepoPath

    $status = git status --porcelain
    if ($status) {
        try {
            git add .
            $commitMessage = "Project Updated"
            git commit -m $commitMessage

            # Simpler: no rebase, just force push
            git push origin $Branch
            Write-Host "✅ Changes pushed at $(Get-Date -Format 'HH:mm:ss')"
        }
        catch {
            Write-Host "⚠️ Git error: $($_.Exception.Message)"
        }
    } else {
        Write-Host "⏳ No changes at $(Get-Date -Format 'HH:mm:ss')"
    }

    Start-Sleep -Seconds 4
}
