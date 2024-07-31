--- 释放召唤师技能
--- @param handleList Team[] 按顺序扫描的队伍
function CheckAndActiveMasterSkill(handleList)
    for _, team in pairs(handleList) do
        local queuedSkillName = FB.CheckMasterSkillApply()
        if queuedSkillName then
            ---@type Skill
            local skill = team.MasterSkills[queuedSkillName]
            local sp = team.Main.Props[Props.SP]
            -- 检查冷却和 SP，冷却由时钟管理
            if skill.CoolDown == 0 and sp >= skill.SP then
                local skillIns = LoadSkill(skill.Name)
                local activated, _ = skillIns.Active()
                if activated then
                    FB.PlayMasterSkillEffect(team.Id, skill.Name)
                    Clock.Tick()
                    FB.Render()
                    -- 释放完成后减少对应 SP
                    team.Main.Props[Props.SP] = sp - skill.SP
                    Clock.Tick()
                    FB.Render()
                    -- 重新计算冷却
                    if skillIns.Cooldown then
                        skillIns.Cooldown()
                    end
                    FB.NotifyMasterSkillApplied()
                    FB.Render()
                end
            end
        end
    end
end
