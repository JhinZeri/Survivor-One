# G_SurvivorOne

## 基于Youtube博主-골드메탈，@goldmetal-像素类幸存者教程
- 作者网站链接：[https://goldmetal.co.kr/](https://goldmetal.co.kr/)
- Youtube视频合集：[유니티 기초 뱀서라이크🧟언데드서바이버](https://www.youtube.com/playlist?list=PLO-mt5Iu5TeZF8xMHqtT_DhAPKmjF6i3x)
- 项目阶段一目标为参考其思路，根据个人习惯开发，完成教程包含的功能内容

## 已使用免费素材链接

- 像素幸存者资源包：[https://assetstore.unity.com/packages/2d/undead-survivor-assets-pack-238068](https://assetstore.unity.com/packages/2d/undead-survivor-assets-pack-238068)
- 像素水果资源包：[https://assetstore.unity.com/packages/2d/gui/icons/2d-pixel-art-icons-fruits-258332](https://assetstore.unity.com/packages/2d/gui/icons/2d-pixel-art-icons-fruits-258332)
## 开发技术和规范记录

- 练习项目，不一定是最简洁实用的方法
- Unity 2023.2.0f1c1
- Input System
- Sensor Toolkit 2
- Cinemachine
- 2D Tilemaps

### 数据

- 初始地图大小为 40 * 30

## 开发日志

### 23-12-15

- 建立Github仓库，完成链接
- 新建Unity项目，Unity 2022.3.14 LTS
- 新增美术资源，像素幸存者和水果图标

### 23-12-16

- 修改项目为新版输入系统 Input System，并学习测试

### 23-12-17

- 修改项目为2023.2.0 f1c1
- 完成角色基础移动控制

### 23-12-18

- 完成角色基础动画控制
- 完成基础地图设置，瓦片地图结构设置
- 完成基础敌人追踪玩家，特性：碰到玩家角色速度为Vector2.zero，不击退。敌人添加了RangeSensor2D组件

### 23-12-19

- Fix：修复敌人重叠问题，修改Rigidbody为Dynamic，消耗更大，同时只保证单个敌人接触玩家角色时不推动玩家

### 23-12-20

- 优化简易对象池逻辑，对象池应该只包含通用逻辑，细节逻辑要交给具体的类
- 实现敌人随时间自动生成，且设置关卡等级雏形
- 实现部分对象数据，使用ScriptableObject资产文件保存

### 23-12-21

- 实现基础武器的铲子生成以及运动
- 初步实现敌人血量计算逻辑
- 实现简易升级逻辑
- 优化对象池Pool，新增Despawn回收方法
- Fix：移除敌人受伤无敌时间

### 23-12-22

- 新增实现远程武器的攻击逻辑

### 23-12-24

- 新增敌人死亡动画，死亡计数，人物等级

### 23-12-25

- 新增杀敌升级HUD，武器升级

### 实现人物自动攻击，升级，结束项目进程