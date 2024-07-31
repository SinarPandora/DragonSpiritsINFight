--- 扫描 Buff
--- @param isBeforeRun boolean 是否处于前置扫描阶段
--- @param handleList Team[] 按顺序扫描的队伍
---@type fun(isBeforeRun: boolean, handleList: Team[])
function BuffScan(isBeforeRun, handleList)
    local isBuffRun = false

    -- 处理场地 Buff
    for name, buff in pairs(Field.FieldBuffs) do
        if not buff.IsAbilityEffect and buff.IsBeforeAction == isBeforeRun then
            local buffIns = LoadBuff(name)
            if buffIns.RunForField(Field) then
                FB.PlayBuffEffect(nil, '场地', name)
                FB.Render()
                isBuffRun = true
            end
        end
    end

    if isBuffRun then
        Clock.Tick()
    end

    -- 处理前置 Buff
    for _, team in pairs(handleList) do
        -- 处理队伍 Buff
        isBuffRun = false
        for name, buff in pairs(team.Buffs) do
            if not buff.IsAbilityEffect and buff.IsBeforeAction == isBeforeRun then
                local buffIns = LoadBuff(name)
                if buffIns.RunForTeam(team) then
                    FB.PlayBuffEffect(team.Id, '队伍', name)
                    FB.Render()
                    isBuffRun = true
                end
            end
        end

        if isBuffRun then
            Clock.Tick()
        end

        isBuffRun = false
        -- 处理前场角色 Buff
        for name, buff in pairs(team.Main.Buffs) do
            if not buff.IsAbilityEffect and buff.IsBeforeAction == isBeforeRun then
                local buffIns = LoadBuff(name)
                if buffIns.RunForActor(team.Main) then
                    FB.PlayBuffEffect(team.Id, '单位', name)
                    FB.Render()
                    isBuffRun = true
                end
            end
        end

        if isBuffRun then
            Clock.Tick()
        end
    end
end
