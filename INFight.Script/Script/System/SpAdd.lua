--- 追加 SP
--- @param handleList Team[] 按顺序扫描的队伍
function AddSp(handleList)
    for _, team in pairs(handleList) do
        local spUp = 1
        ---@type string[]
        ---@diagnostic disable-next-line: assign-type-mismatch
        local actions = team.Main.Props[Props.SPActions]

        if actions then
            for _, action in ipairs(actions) do
                local add, err = load(action)
                if add then
                    spUp = add(spUp)
                else
                    Log.Error("脚本出错：" .. err)
                    FB.Panic("SP脚本执行出错，决斗失败，请反馈该错误")
                end
            end
            -- 清理脚本
            team.Main.Props[Props.SPActions] = {}
        end

        team.Main.Props[Props.SP] = team.Main.Props[Props.SP] + spUp
        Clock.Tick()
        FB.Render()
    end
end
