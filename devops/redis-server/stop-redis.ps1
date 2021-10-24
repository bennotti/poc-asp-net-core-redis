$projectName=$args[0]
$ambiente=$args[1]
write-host "Parametros defaults, Sintaxe customizada: run-redis.ps1 projectName local|dev|tst|hml|prd" 
write-host "Parametros defaults, default: run-redis.ps1 projectName local" 

if ($projectName -eq $null) {
    $projectName = "projectName"
}

if ($ambiente -eq $null) {
    $ambiente = "local"
}

$dockerProjectName=$projectName + '_redis_' + $ambiente
$dockerFileName='docker-comp-redis-' + $ambiente + '.yml'

docker-compose -p $dockerProjectName -f ./devops/redis-server/$dockerFileName down