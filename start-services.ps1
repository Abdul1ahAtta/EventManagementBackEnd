# Kill any existing service processes
taskkill /F /IM UserService.exe 2>$null
taskkill /F /IM EventService.exe 2>$null
taskkill /F /IM BookingService.exe 2>$null

# Start UserService
$userServiceWindow = {
    Set-Location -Path "UserService"
    dotnet run --urls="http://localhost:5048"
}
Start-Process powershell -ArgumentList "-NoExit", "-Command", $userServiceWindow

# Start EventService
$eventServiceWindow = {
    Set-Location -Path "EventService"
    dotnet run --urls="http://localhost:5036"
}
Start-Process powershell -ArgumentList "-NoExit", "-Command", $eventServiceWindow

# Start BookingService
$bookingServiceWindow = {
    Set-Location -Path "BookingService"
    dotnet run --urls="http://localhost:5049"
}
Start-Process powershell -ArgumentList "-NoExit", "-Command", $bookingServiceWindow

Write-Host "All services started. Check the individual windows for status." 