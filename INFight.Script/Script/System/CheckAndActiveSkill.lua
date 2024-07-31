--- 检查 SP 量并释放技能
--- @param handleList Team[] 按顺序扫描的队伍
function CheckAndActiveSkill(handleList)
    for _, team in pairs(handleList) do
        local actor = team.Main
        local skill = actor.SkillSeq[actor.SkillSeqIndex]
        local sp = team.Main.Props[Props.SP]

        if skill.CoolDown == 0 and sp >= skill.SP then
            local skillIns = LoadSkill(skill.Name)
            local activated, indexUp = skillIns.Active()
            if activated then
                FB.PlaySkillEffect(team.Id, skill.Name)
                FB.Render()
                team.Main.Props[Props.SP] = sp - skill.SP
                Clock.Tick()
                FB.Render()
                -- 表示要替换技能
                if indexUp ~= 0 then
                    local newIndex = actor.SkillSeqIndex + indexUp

                    if newIndex <= 1 or newIndex > #actor.SkillSeq then
                        -- 处理越界
                        actor.SkillSeqIndex = 1
                    else
                        actor.SkillSeqIndex = newIndex
                    end
                end
                FB.Render()
                if skillIns.Cooldown then
                    skillIns.Cooldown()
                end
                Clock.Tick()
                FB.Render()
            end
        end
    end
end
