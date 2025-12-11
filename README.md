# com.dave6.unity-utils
**Unity** 관련 유틸리티 패키지.

## Feature

- 런타임에 Unity의 **PlayerLoop** 를 수정하는 작은 유틸리티.
특정 타이밍(예: `EarlyUpdate`, `Update`, `PreLateUpdate` 등)에 자신의 업데이트 로직을 삽입가능.

- GameObject의 `GetOrAddComponent` 함수로 간단 컴포넌트 참조 함수 제공

## Usage
`PlayerLoopUtils` 다음과 같은 보조 방법을 제공:
- 사용자 지정 업데이트 추가/제거
- 수정한 **PlayerLoop** 를 Unity에 적용

Basic flow:

```csharp

// ==================================
// 초기화 단계
// ==================================
// 현재 시스템 가져옴
internal static void Initialize()
{
    PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

    // 내 시스템 정의
    int order = 0; // 가장 먼저
    mySystem = new PlayerLoopSystem() // PlayerLoopSystem mySystem;
    {
        type = typeof(MyGameManager),
        subSystemList = null,
        updateDelegate = MyGameManager.UpdateLogic,
    };
    // PlayerLoop에 추가
    PlayerLoopUtils.InsertSystem<T>(ref currentPlayerLoop, in mySystem, order);

    // 시스템 적용 후 덧씌우기
    PlayerLoop.SetPlayerLoop(currentPlayerLoop);

#if UNITY_EDITOR
    // 시스템 중복등록 방지
    EditorApplication.playModeStateChanged -= OnPlayModeState;
    EditorApplication.playModeStateChanged += OnPlayModeState;
#endif
    static void OnPlayModeState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
            PlayerLoopUtils.RemoveSystem<T>(ref currentPlayerLoop, in mySystem);
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);

            MyGameManager.Clear();
        }
    }
}


```
런타임 업데이트 순서를 간단하고 직접적으로 제어해야 할 때 사용.