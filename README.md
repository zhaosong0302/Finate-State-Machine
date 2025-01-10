# Finate-State-Machine
代码实现有限状态机达到AI功能，实现巡逻、追逐、攻击、逃跑逻辑功能，并可在原有架构上自由拓展----Implement finate state machine by using code to achieve AI function, including patrol, chase, attack, return. Also expanding is free.

----有限状态机介绍----

有限状态机(Finite - state machine,FSM)又称有限状态自动机。简称状态机
是表示有限个状态以及在这些状态之间的转移和动作等行为的数学模型
有限:表示是有限度的不是无限的
状态:指所拥有的所有状态

举例说明:
假设我们人会做很多个动作，也就是有很多种状态
这些状态包括站立、走路、跑步、攻击、防守、睡觉等等
我们每天都会在这些状态中切换,而且这些状态虽然多但是是有限的当达到某种条件时，我们就会在这些状态中进行切换
而且这种切换时随时可能发生的

----框架设计----

主要通过几个类实现功能

1.状态机类--StateMachine
用于管理各个状态，切换状态方法在此调用

2.状态基类--BaseState
所有状态类继承自共有的状态基类，包含所有类共有的三个基本逻辑处理，进入状态EnterState()，处于状态UpdateState()，离开状态QuitState(),离开逻辑在UpdateState()中判断

3.状态类--EscapeState, ChaseState, PatrolState, AtkState
各个状态的逻辑功能实现，作为框架加入了逃跑、追逐、巡逻、攻击四个逻辑进行测试

4.枚举类--EAIState
罗列各个状态名，方便处理

5.AI基接口--IAIObj
所有需要使用此AI功能的游戏物体继承此类，在此类中提取共有动作。出于测试需要，在此类中引入NavMeshAgent寻路组件，使用NMA进行移动测试。

如图
![N%}COV8(OT40VD(W~W6_YBG](https://github.com/user-attachments/assets/2d77f314-0f51-4d38-b1b3-a9b16398edbc)

表现效果

巡逻中 包含随机点巡逻、多点循环巡逻，此处展示效果为多点循环巡逻
![}WV_3@JTWAQH~0DAOW005UH](https://github.com/user-attachments/assets/8a9db382-aef4-4a92-b6e2-68a58512caa1)

进入追逐范围后追逐
![VJ8TUMYBGJ5XS988(TXKCHD](https://github.com/user-attachments/assets/65189389-7820-432a-895b-55a96193b862)

进入攻击范围后攻击
![$`JU422T76} CB4H3S5Y26K](https://github.com/user-attachments/assets/827951a2-5f55-4f6e-8be6-c68a794da261)

超出最大范围回归
![~ FUM$F_EI151JD4}TNNA~8](https://github.com/user-attachments/assets/b15a73de-9a11-4416-979f-6765cfe291e5)
