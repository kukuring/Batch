echo OFF
echo.
echo.
echo ------------------------------------------------------------
echo --------------���ð� �Է�-----------------------------------
echo.
set /p id=���(�빮��)�� �Է����ּ���:
echo.
set /p checkInTime=��ٽð�(���� hh:mm / Ex. 09:00)�� �Է����ּ���:
echo.
set /p checkOutTime=��ٽð�(���� hh:mm / Ex. 18:00)�� �Է����ּ���:
echo.
set /p useCheckInAutoCall=���üũ�� �ڵ����� �Ͻðڽ��ϱ�?(true or false - default false):
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------���ð� Ȯ��-----------------------------------
set fileName=WorkTimeCheckAlarmSetting.json
echo ------------------------------------------------------------
echo.
echo �Է��� ���: %id%
echo �Է��� ��ٽð�: %checkInTime%
echo �Է��� ��ٽð�: %checkOutTime%
echo �Է��� �ڵ�����ٿ���: %useCheckInAutoCall%
echo �߸� �Է��Ͽ��� ��� %fileName% ���� ���� �������ּ���.
echo.
echo.
echo ------------------------------------------------------------
echo --------------�������� ����---------------------------------
del %fileName% /q
echo ------------------------------------------------------------
echo.
echo.
echo ------------------------------------------------------------
echo --------------�������� ����---------------------------------
echo {>> %fileName%
echo    "checkInTime": "%checkInTime%",>> %fileName%
echo    "checkOutTime": "%checkOutTime%",>> %fileName%
echo    "id": "%id%",>> %fileName%
echo    "useCheckInAutoCall": "%useCheckInAutoCall%",>> %fileName%
echo    "myHoliday": [>> %fileName%
echo           "2019-01-01",>> %fileName%
echo           "2019-01-02">> %fileName%
echo     ],>> %fileName%
echo    "holiday": [>> %fileName%
echo           { "date": "2019-01-01", "name": "����" },>> %fileName%
echo           { "date": "2019-02-04", "name": "��" },>> %fileName%
echo           { "date": "2019-02-05", "name": "��" },>> %fileName%
echo           { "date": "2019-02-06", "name": "��" },>> %fileName%
echo           { "date": "2019-03-01", "name": "������" },>> %fileName%
echo           { "date": "2019-05-01", "name": "�ٷ����� ��" },>> %fileName%
echo           { "date": "2019-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2019-05-06", "name": "��ü������(��̳�)" },>> %fileName%
echo           { "date": "2019-05-12", "name": "����ź����" },>> %fileName%
echo           { "date": "2019-06-06", "name": "������" },>> %fileName%
echo           { "date": "2019-08-15", "name": "������" },>> %fileName%
echo           { "date": "2019-09-12", "name": "�߼�" },>> %fileName%
echo           { "date": "2019-09-13", "name": "�߼�" },>> %fileName%
echo           { "date": "2019-09-14", "name": "�߼�" },>> %fileName%
echo           { "date": "2019-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2019-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2019-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2020-01-01", "name": "����" },>> %fileName%
echo           { "date": "2020-01-24", "name": "��" },>> %fileName%
echo           { "date": "2020-01-25", "name": "��" },>> %fileName%
echo           { "date": "2020-01-26", "name": "��" },>> %fileName%
echo           { "date": "2020-03-01", "name": "������" },>> %fileName%
echo           { "date": "2020-04-30", "name": "����ź����" },>> %fileName%
echo           { "date": "2020-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2020-05-30", "name": "����ź����" },>> %fileName%
echo           { "date": "2020-06-06", "name": "������" },>> %fileName%
echo           { "date": "2020-08-15", "name": "������" },>> %fileName%
echo           { "date": "2020-09-30", "name": "�߼�" },>> %fileName%
echo           { "date": "2020-10-01", "name": "�߼�" },>> %fileName%
echo           { "date": "2020-10-02", "name": "�߼�" },>> %fileName%
echo           { "date": "2020-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2020-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2020-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2021-01-01", "name": "����" },>> %fileName%
echo           { "date": "2021-02-11", "name": "��" },>> %fileName%
echo           { "date": "2021-02-12", "name": "��" },>> %fileName%
echo           { "date": "2021-02-13", "name": "��" },>> %fileName%
echo           { "date": "2021-03-01", "name": "������" },>> %fileName%
echo           { "date": "2021-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2021-05-19", "name": "����ź����" },>> %fileName%
echo           { "date": "2021-06-06", "name": "������" },>> %fileName%
echo           { "date": "2021-08-15", "name": "������" },>> %fileName%
echo           { "date": "2021-09-20", "name": "�߼�" },>> %fileName%
echo           { "date": "2021-09-21", "name": "�߼�" },>> %fileName%
echo           { "date": "2021-09-22", "name": "�߼�" },>> %fileName%
echo           { "date": "2021-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2021-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2021-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2022-01-01", "name": "����" },>> %fileName%
echo           { "date": "2022-01-31", "name": "��" },>> %fileName%
echo           { "date": "2022-02-01", "name": "��" },>> %fileName%
echo           { "date": "2022-02-02", "name": "��" },>> %fileName%
echo           { "date": "2022-03-01", "name": "������" },>> %fileName%
echo           { "date": "2022-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2022-05-08", "name": "����ź����" },>> %fileName%
echo           { "date": "2022-06-06", "name": "������" },>> %fileName%
echo           { "date": "2022-08-15", "name": "������" },>> %fileName%
echo           { "date": "2022-09-09", "name": "�߼�" },>> %fileName%
echo           { "date": "2022-09-10", "name": "�߼�" },>> %fileName%
echo           { "date": "2022-09-11", "name": "�߼�" },>> %fileName%
echo           { "date": "2022-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2022-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2022-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2023-01-01", "name": "����" },>> %fileName%
echo           { "date": "2023-01-21", "name": "��" },>> %fileName%
echo           { "date": "2023-01-22", "name": "��" },>> %fileName%
echo           { "date": "2023-01-23", "name": "��" },>> %fileName%
echo           { "date": "2023-03-01", "name": "������" },>> %fileName%
echo           { "date": "2023-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2023-05-27", "name": "����ź����" },>> %fileName%
echo           { "date": "2023-06-06", "name": "������" },>> %fileName%
echo           { "date": "2023-08-15", "name": "������" },>> %fileName%
echo           { "date": "2023-09-28", "name": "�߼�" },>> %fileName%
echo           { "date": "2023-09-29", "name": "�߼�" },>> %fileName%
echo           { "date": "2023-09-30", "name": "�߼�" },>> %fileName%
echo           { "date": "2023-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2023-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2023-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2024-01-01", "name": "����" },>> %fileName%
echo           { "date": "2024-02-09", "name": "��" },>> %fileName%
echo           { "date": "2024-02-10", "name": "��" },>> %fileName%
echo           { "date": "2024-02-11", "name": "��" },>> %fileName%
echo           { "date": "2024-03-01", "name": "������" },>> %fileName%
echo           { "date": "2024-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2024-05-15", "name": "����ź����" },>> %fileName%
echo           { "date": "2024-06-06", "name": "������" },>> %fileName%
echo           { "date": "2024-08-15", "name": "������" },>> %fileName%
echo           { "date": "2024-09-16", "name": "�߼�" },>> %fileName%
echo           { "date": "2024-09-17", "name": "�߼�" },>> %fileName%
echo           { "date": "2024-09-18", "name": "�߼�" },>> %fileName%
echo           { "date": "2024-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2024-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2024-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2025-01-01", "name": "����" },>> %fileName%
echo           { "date": "2025-01-28", "name": "��" },>> %fileName%
echo           { "date": "2025-01-29", "name": "��" },>> %fileName%
echo           { "date": "2025-01-30", "name": "��" },>> %fileName%
echo           { "date": "2025-03-01", "name": "������" },>> %fileName%
echo           { "date": "2025-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2025-06-06", "name": "������" },>> %fileName%
echo           { "date": "2025-08-15", "name": "������" },>> %fileName%
echo           { "date": "2025-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2025-10-05", "name": "�߼�" },>> %fileName%
echo           { "date": "2025-10-06", "name": "�߼�" },>> %fileName%
echo           { "date": "2025-10-07", "name": "�߼�" },>> %fileName%
echo           { "date": "2025-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2025-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2026-01-01", "name": "����" },>> %fileName%
echo           { "date": "2026-02-16", "name": "��" },>> %fileName%
echo           { "date": "2026-02-17", "name": "��" },>> %fileName%
echo           { "date": "2026-02-18", "name": "��" },>> %fileName%
echo           { "date": "2026-03-01", "name": "������" },>> %fileName%
echo           { "date": "2026-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2026-05-24", "name": "����ź����" },>> %fileName%
echo           { "date": "2026-06-06", "name": "������" },>> %fileName%
echo           { "date": "2026-08-15", "name": "������" },>> %fileName%
echo           { "date": "2026-09-24", "name": "�߼�" },>> %fileName%
echo           { "date": "2026-09-25", "name": "�߼�" },>> %fileName%
echo           { "date": "2026-09-26", "name": "�߼�" },>> %fileName%
echo           { "date": "2026-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2026-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2026-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2027-01-01", "name": "����" },>> %fileName%
echo           { "date": "2027-02-06", "name": "��" },>> %fileName%
echo           { "date": "2027-02-07", "name": "��" },>> %fileName%
echo           { "date": "2027-02-08", "name": "��" },>> %fileName%
echo           { "date": "2027-03-01", "name": "������" },>> %fileName%
echo           { "date": "2027-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2027-05-13", "name": "����ź����" },>> %fileName%
echo           { "date": "2027-06-06", "name": "������" },>> %fileName%
echo           { "date": "2027-08-15", "name": "������" },>> %fileName%
echo           { "date": "2027-09-14", "name": "�߼�" },>> %fileName%
echo           { "date": "2027-09-15", "name": "�߼�" },>> %fileName%
echo           { "date": "2027-09-16", "name": "�߼�" },>> %fileName%
echo           { "date": "2027-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2027-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2027-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2028-01-01", "name": "����" },>> %fileName%
echo           { "date": "2028-01-26", "name": "��" },>> %fileName%
echo           { "date": "2028-01-27", "name": "��" },>> %fileName%
echo           { "date": "2028-01-28", "name": "��" },>> %fileName%
echo           { "date": "2028-03-01", "name": "������" },>> %fileName%
echo           { "date": "2028-05-02", "name": "����ź����" },>> %fileName%
echo           { "date": "2028-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2028-06-06", "name": "������" },>> %fileName%
echo           { "date": "2028-08-15", "name": "������" },>> %fileName%
echo           { "date": "2028-10-02", "name": "�߼�" },>> %fileName%
echo           { "date": "2028-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2028-10-04", "name": "�߼�" },>> %fileName%
echo           { "date": "2028-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2028-12-25", "name": "ũ��������" },>> %fileName%
echo           { "date": "2029-01-01", "name": "����" },>> %fileName%
echo           { "date": "2029-02-12", "name": "��" },>> %fileName%
echo           { "date": "2029-02-13", "name": "��" },>> %fileName%
echo           { "date": "2029-02-14", "name": "��" },>> %fileName%
echo           { "date": "2029-03-01", "name": "������" },>> %fileName%
echo           { "date": "2029-05-05", "name": "��̳�" },>> %fileName%
echo           { "date": "2029-05-20", "name": "����ź����" },>> %fileName%
echo           { "date": "2029-06-06", "name": "������" },>> %fileName%
echo           { "date": "2029-08-15", "name": "������" },>> %fileName%
echo           { "date": "2029-09-21", "name": "�߼�" },>> %fileName%
echo           { "date": "2029-09-22", "name": "�߼�" },>> %fileName%
echo           { "date": "2029-09-23", "name": "�߼�" },>> %fileName%
echo           { "date": "2029-10-03", "name": "��õ��" },>> %fileName%
echo           { "date": "2029-10-09", "name": "�ѱ۳�" },>> %fileName%
echo           { "date": "2029-12-25", "name": "ũ��������" }>> %fileName%
echo     ]>> %fileName%
echo }>> %fileName%
echo ------------------------------------------------------------
echo.
echo.
echo ------------------------------------------------------------
echo ---��������(%fileName%)��  ��ġ������ �����Ǿ����ϴ�.----
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------�������� ����-------------------------------
net stop "WorkTimeCheckAlarmService"
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------�������� ����-------------------------------
sc delete "WorkTimeCheckAlarmService"
rem DO NOT remove the space after "binpath="!
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------���� ��ġ-----------------------------------
sc create "WorkTimeCheckAlarmService" binpath= "C:\Program Files\Inpark\WorkTimeCheckAlram\WorkTimeCheckAlarmService.exe" start= auto
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------���� ����-----------------------------------
sc start WorkTimeCheckAlarmService
echo ------------------------------------------------------------

echo.
echo.
echo ------------------------------------------------------------
echo --------------�۾� �Ϸ�-------------------------------------
echo ------------------------------------------------------------
pause