$path = "c:\Users\marni\Downloads\0kUGwgRW - project-1-databases (1).json"
$json = Get-Content -Raw -Path $path | ConvertFrom-Json

$lists = @{}
if ($json.lists) {
    foreach ($l in $json.lists) { $lists[$l.id] = $l.name }
}

$checklists = @{}
if ($json.checklists) {
    foreach ($cl in $json.checklists) { $checklists[$cl.id] = $cl }
}

Write-Host "# Trello Board: $($json.name)"

$targetLists = @("Backlog", "to do", "doing", "Review", "done")

foreach ($listName in $targetLists) {
    $listId = $null
    foreach ($key in $lists.Keys) {
        if ($lists[$key] -eq $listName) {
            $listId = $key
            break
        }
    }
    
    if (-not $listId) { continue }
    
    Write-Host "`n## List: $listName"
    
    if ($json.cards) {
        foreach ($card in $json.cards) {
            if ($card.idList -eq $listId -and $card.closed -eq $false) {
                Write-Host "`n### $($card.name)"
                if ($card.desc) { Write-Host "**Description:**`n$($card.desc)`n" }
                
                if ($card.idChecklists) {
                    foreach ($clid in $card.idChecklists) {
                        $cl = $checklists[$clid]
                        if ($cl) {
                            Write-Host "**Checklist: $($cl.name)**"
                            if ($cl.checkItems) {
                                foreach ($item in $cl.checkItems) {
                                    $state = if ($item.state -eq 'complete') { "[x]" } else { "[ ]" }
                                    Write-Host "  $state $($item.name)"
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
