



echo OFF
echo Stopping old service version...
net stop "WorkTimeCheckAlarmService"
echo Uninstalling old service version...
sc delete "WorkTimeCheckAlarmService"
pause