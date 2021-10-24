#!/bin/sh

set -x;
echo $USER

if  [ $# -lt 2 ]; then
    echo "Parametros defaults, Sintaxe customizada: sh run-redis.sh projectName local|dev|tst|hml|prd"
    echo "Parametros defaults, default: sh run-redis.sh projectName local"
    projectName="projectName"
    ambiente="local"
else 
    projectName=$1
    ambiente=$2
fi

dockerProjectName=$projectName'_redis_'$ambiente
dockerFileName='docker-comp-redis-'$ambiente'.yml'

echo "#### docker-compose down"
docker-compose -p $dockerProjectName -f ./$dockerFileName down

echo "#### docker-compose up --build -d"
docker-compose -p $dockerProjectName -f ./$dockerFileName up --build -d