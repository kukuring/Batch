echo OFF
echo.
echo.
echo ------------------------------------------------------------
echo --------------셋팅값 입력-----------------------------------
echo.
set /p id=사번(대문자)을 입력해주세요:
echo.
set /p checkInTime=출근시간(형식 hh:mm / Ex. 09:00)을 입력해주세요:
echo.
set /p checkOutTime=퇴근시간(형식 hh:mm / Ex. 18:00)을 입력해주세요:
echo.
set /p useCheckInAutoCall=출근체크를 자동으로 하시겠습니까?(true or false - default false):
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------셋팅값 확인-----------------------------------
set fileName=WorkTimeCheckAlarmSetting.json
echo ------------------------------------------------------------
echo.
echo 입력한 사번: %id%
echo 입력한 출근시간: %checkInTime%
echo 입력한 퇴근시간: %checkOutTime%
echo 입력한 자동출퇴근여부: %useCheckInAutoCall%
echo 잘못 입력하였을 경우 %fileName% 에서 값을 수정해주세요.
echo.
echo.
echo ------------------------------------------------------------
echo --------------기존파일 삭제---------------------------------
del %fileName% /q
echo ------------------------------------------------------------
echo.
echo.
echo ------------------------------------------------------------
echo --------------셋팅파일 생성---------------------------------
echo {>> %fileName%
echo    "checkInTime": "%checkInTime%",>> %fileName%
echo    "checkOutTime": "%checkOutTime%",>> %fileName%
echo    "id": "%id%",>> %fileName%
echo    "useCheckInAutoCall": "%useCheckInAutoCall%",>> %fileName%
echo    "myHoliday": [>> %fileName%
echo           { "date": "2019-01-01", "type": "ALL", "name": "" },>> %fileName%
echo           { "date": "2019-01-02", "type": "AM", "name": "" },>> %fileName%
echo           { "date": "2019-01-03", "type": "PM", "name": "" }>> %fileName%
echo     ],>> %fileName%
echo    "holiday": [>> %fileName%
echo           { "date": "2019-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2019-02-04", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2019-02-05", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2019-02-06", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2019-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2019-05-01", "type": "ALL", "name": "근로자의 날" },>> %fileName%
echo           { "date": "2019-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2019-05-06", "type": "ALL", "name": "대체공휴일(어린이날)" },>> %fileName%
echo           { "date": "2019-05-12", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2019-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2019-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2019-09-12", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2019-09-13", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2019-09-14", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2019-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2019-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2019-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2020-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2020-01-24", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2020-01-25", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2020-01-26", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2020-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2020-04-30", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2020-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2020-05-30", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2020-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2020-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2020-09-30", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2020-10-01", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2020-10-02", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2020-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2020-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2020-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2021-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2021-02-11", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2021-02-12", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2021-02-13", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2021-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2021-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2021-05-19", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2021-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2021-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2021-09-20", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2021-09-21", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2021-09-22", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2021-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2021-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2021-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2022-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2022-01-31", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2022-02-01", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2022-02-02", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2022-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2022-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2022-05-08", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2022-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2022-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2022-09-09", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2022-09-10", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2022-09-11", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2022-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2022-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2022-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2023-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2023-01-21", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2023-01-22", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2023-01-23", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2023-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2023-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2023-05-27", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2023-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2023-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2023-09-28", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2023-09-29", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2023-09-30", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2023-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2023-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2023-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2024-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2024-02-09", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2024-02-10", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2024-02-11", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2024-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2024-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2024-05-15", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2024-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2024-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2024-09-16", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2024-09-17", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2024-09-18", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2024-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2024-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2024-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2025-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2025-01-28", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2025-01-29", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2025-01-30", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2025-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2025-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2025-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2025-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2025-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2025-10-05", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2025-10-06", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2025-10-07", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2025-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2025-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2026-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2026-02-16", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2026-02-17", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2026-02-18", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2026-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2026-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2026-05-24", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2026-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2026-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2026-09-24", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2026-09-25", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2026-09-26", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2026-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2026-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2026-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2027-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2027-02-06", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2027-02-07", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2027-02-08", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2027-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2027-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2027-05-13", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2027-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2027-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2027-09-14", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2027-09-15", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2027-09-16", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2027-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2027-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2027-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2028-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2028-01-26", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2028-01-27", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2028-01-28", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2028-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2028-05-02", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2028-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2028-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2028-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2028-10-02", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2028-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2028-10-04", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2028-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2028-12-25", "type": "ALL", "name": "크리스마스" },>> %fileName%
echo           { "date": "2029-01-01", "type": "ALL", "name": "신정" },>> %fileName%
echo           { "date": "2029-02-12", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2029-02-13", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2029-02-14", "type": "ALL", "name": "설" },>> %fileName%
echo           { "date": "2029-03-01", "type": "ALL", "name": "삼일절" },>> %fileName%
echo           { "date": "2029-05-05", "type": "ALL", "name": "어린이날" },>> %fileName%
echo           { "date": "2029-05-20", "type": "ALL", "name": "석가탄신일" },>> %fileName%
echo           { "date": "2029-06-06", "type": "ALL", "name": "현충일" },>> %fileName%
echo           { "date": "2029-08-15", "type": "ALL", "name": "광복절" },>> %fileName%
echo           { "date": "2029-09-21", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2029-09-22", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2029-09-23", "type": "ALL", "name": "추석" },>> %fileName%
echo           { "date": "2029-10-03", "type": "ALL", "name": "개천절" },>> %fileName%
echo           { "date": "2029-10-09", "type": "ALL", "name": "한글날" },>> %fileName%
echo           { "date": "2029-12-25", "type": "ALL", "name": "크리스마스" }>> %fileName%
echo     ]>> %fileName%
echo }>> %fileName%
echo ------------------------------------------------------------
echo.
echo.
echo ------------------------------------------------------------
echo ---설정파일(%fileName%)이  설치폴더에 생성되었습니다.----
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------기존서비스 중지-------------------------------
net stop "WorkTimeCheckAlarmService"
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------기존서비스 삭제-------------------------------
sc delete "WorkTimeCheckAlarmService"
rem DO NOT remove the space after "binpath="!
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------서비스 설치-----------------------------------
sc create "WorkTimeCheckAlarmService" binpath= "C:\Program Files\Inpark\WorkTimeCheckAlram\WorkTimeCheckAlarmService.exe" start= auto
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------서비스 시작-----------------------------------
sc start WorkTimeCheckAlarmService
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------작업 완료-------------------------------------
echo ------------------------------------------------------------
pause
notepad Notice.txt