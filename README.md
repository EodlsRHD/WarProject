# WarProject

◼ 현재 구현된 것

- 월드
  1. 생성된 월드에 Node생성 및 Chunk로 묶음관리
  2. 씬 전환 후 오브젝트를 세팅해줄 WorldDetectionManager 구현
  
- 오브젝트
  1. Object의 CampType에 따라서 외관 구분 적용
  2. 다량 생산되는 Object의 ObjectPooling적용
  3. Object세부 기능 구분
  
-UI
  1. 캐릭터 선택 가능
  2. 맵 선택 가능
  
- Data
  1. (없음)

◼ 구현 목록

- 월드
  1. 에셋 구매후 필드 적용 및 꾸미기
  2. Fog of War
  3. 적을 감지하고 병력을 보내는 AI
  4. 군집 이동 알고리즘
  
- 오브젝트
  1. 에셋 구매후 외관 적용 및 Animation 적용
  2. Building에서 생산되는 유닛수 늘리기
  3. Particle 적용
  
  -UI
  1. 에셋 구매후 UI 제작
  2. 미니맵
  3. 레디얼 메뉴
  
- Data
  1. 서버 구축 및 데이터 전송(Json)
