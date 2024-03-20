$quizIDs = @(curl  -Uri "https://create.kahoot.it/rest/kahoots/?query=&limit=100&orderBy=number_of_players&cursor=0&searchCluster=1&includeExtendedCounters=false&inventoryItemId=ANY"  | Select-Object -Expand Content | jq '.entities[] | .card.uuid')
#'[' | Out-File -FilePath ./kahootResult.json -Append
foreach($ID in $quizIds) {
    $IDTrim = $ID.Trim('"')
    $url = "https://create.kahoot.it/rest/kahoots/$IDTrim"
    curl -Uri $($url) | Select-Object -Expand Content | jq --arg type 'quiz'  '. | {title: .title, description: .description, imageurl: .cover, questions: [.questions[] | select(.type==$type) | {value: .question, time: .time, imageurl: .image, choices: [.choices[] | {answer: .answer, correct: .correct}]}]}' | Out-File -FilePath ./kahootResult.json -Append
    "," | Out-File -FilePath ./kahootResult.json -Append
}
#']' | Out-File -FilePath ./kahootResult.json -Append
