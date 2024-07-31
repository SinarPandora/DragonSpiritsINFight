--- 换人
--- @param team Team 指定队伍
--- @param index number | nil 指定角色
--- @return boolean 是否换人成功
function SwitchActor(team, index)
    -- 检查是否有能替换的龙魂
    if team.Main == index then
        FB.Toast('替换目标是自己，替换无效')
        return false
    end

    if not index or index > #team.Subs then
        for i, actor in ipairs(team.Subs) do
            if actor.Alive then
                index = i
                break
            end
        end
    end

    if not index then
        FB.Toast('已经没有可以替换的龙魂')
        return false
    end

    -- 执行离场脚本
    if team.Main.Alive and team.Main.Props[Props.LeaveScript] then
        ---@diagnostic disable-next-line: param-type-mismatch
        local leave, err = load(team.Main.Props[Props.LeaveScript])
        if leave then
            leave(team.Main)
        else
            Log.Error("离场脚本出错：" .. err)
            FB.Panic("离场技能释放出错，决斗失败，请反馈该错误")
            return false
        end
    end

    local main = team.Main
    local next = team.Subs[index]
    team.Subs[index] = main

    Clock.Tick()
    FB.Render()

    -- 执行登场脚本
    if next.Props[Props.EnterScript] then
        ---@diagnostic disable-next-line: param-type-mismatch
        local enter, err = load(next.Props[Props.EnterScript])
        if enter then
            enter(next)
        else
            Log.Error("登场脚本出错：" .. err)
            FB.Panic("登场技能释放出错，决斗失败，请反馈该错误")
            return false
        end
    end

    -- 重置退场角色能力变化
    if main.Alive then
        for name, buff in pairs(main.Buffs) do
            if buff.IsAbilityEffect then
                main.Buffs[name] = nil
            end
        end
    end

    return true
end
