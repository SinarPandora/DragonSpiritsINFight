---@diagnostic disable-next-line: undefined-global
__loadFile('System/Props.lua')
---@diagnostic disable-next-line: undefined-global
__loadFile('System/BuffScan.lua')
---@diagnostic disable-next-line: undefined-global
__loadFile('System/SpAdd.lua')
---@diagnostic disable-next-line: undefined-global
__loadFile('System/CheckAndActiveMasterSkill.lua')
---@diagnostic disable-next-line: undefined-global
__loadFile('System/CheckAndActiveSkill.lua')

---@type IFeedback
FB = FB or {}
---@type Battlefield
Field = Field or {}
---@type Clock
Clock = Clock or {}
---@type Log
Log = Log or {}
---@type fun(name: string): SkillEffect
LoadSkill = LoadSkill or function(_)
end
---@type fun(name: string): BuffEffect
LoadBuff = LoadBuff or function(_)
end

-- 快速结束
if Field.Finished() then
    return
end

-- 初始化数据
Field.TeamA.Main.Props[Props.SPActions] = Field.TeamA.Main.Props[Props.SPActions] or {}
Field.TeamB.Main.Props[Props.SPActions] = Field.TeamB.Main.Props[Props.SPActions] or {}

-- 判断先手
local aSP = Field.TeamA.Main.Props[Props.SP]
local bSP = Field.TeamB.Main.Props[Props.SP]
local handleList = aSP > bSP and { Field.TeamA, Field.TeamB } or { Field.TeamB, Field.TeamA }

-- 前置 Buff 处理
CheckAndActiveMasterSkill(handleList) -- 插入释放召唤师技能的时点，下文同理
FB.WaitingUiUpdate()
BuffScan(true, handleList)
FB.WaitingUiUpdate()

-- 增加 SP
CheckAndActiveMasterSkill(handleList)
FB.WaitingUiUpdate()
AddSp(handleList)
FB.WaitingUiUpdate()

-- 判断技能释放
CheckAndActiveMasterSkill(handleList)
FB.WaitingUiUpdate()
CheckAndActiveSkill(handleList)
FB.WaitingUiUpdate()

-- 后置 Buff 处理
CheckAndActiveMasterSkill(handleList)
FB.WaitingUiUpdate()
BuffScan(false, handleList)
FB.WaitingUiUpdate()
