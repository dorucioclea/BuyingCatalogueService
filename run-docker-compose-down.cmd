docker-compose -f ".\docker-compose.yml" -f ".\docker-compose.%1.yml" down -v --rmi "all"