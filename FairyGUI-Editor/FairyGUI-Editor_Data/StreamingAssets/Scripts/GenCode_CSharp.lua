local function genCode(handler)
    local settings = handler.project:GetSettings("Publish").codeGeneration
    local codePkgName = handler:ToFilename(handler.pkg.name); --convert chinese to pinyin, remove special chars etc.
    local exportCodePath = handler.exportCodePath..'/HotfixView/Client/Module/UI/'..codePkgName
    local exportCodePath2 = handler.exportCodePath..'/ModelView/Client/Module/UI/'..codePkgName
    local namespaceName = ""
    
    if settings.packageName~=nil and settings.packageName~='' then
        namespaceName = settings.packageName..'.'..namespaceName;
    end

    --CollectClasses(stripeMemeber, stripeClass, fguiNamespace)
    local classes = handler:CollectClasses(settings.ignoreNoname, settings.ignoreNoname, nil)
    handler:SetupCodeFolder(exportCodePath, "cs") --check if target folder exists, and delete old files
    handler:SetupCodeFolder(exportCodePath2, "cs")
    local getMemberByName = settings.getMemberByName

    local classCnt = classes.Count
    local writer = CodeWriter.new()
    for i=0,classCnt-1 do
        local classInfo = classes[i]
        App.consoleView:Log(classInfo.className)
        local members = classInfo.members
        writer:reset()

           writer:writeln('using FairyGUI;')
           writer:writeln()
           writer:writeln('namespace ET.Client')
           writer:startBlock()
           writer:writeln('[EntitySystemOf(typeof(%s))]', classInfo.className)
           writer:writeln('[FriendOf(typeof(%s))]', classInfo.className)
           writer:writeln('public static partial class %sSystem', classInfo.className)
           writer:startBlock()
           writer:writeln('[EntitySystem]')
           writer:writeln('private static void Awake(this %s self, GObject obj)', classInfo.className)
           writer:startBlock()
           writer:writeln('self.com = obj;')
           local memberCnt = members.Count
           for j=0,memberCnt-1 do
               local memberInfo = members[j]
               if memberInfo.group==0 then
                    local isclass = false
                    for z=0,classCnt-1 do
                        if classes[z].className == memberInfo.type then 
                            isclass = true
                        end
                    end
                    if isclass then 
                         if getMemberByName then
                            writer:writeln('self.%s = self.AddComponent<%s, GObject>(obj.asCom.GetChild("%s"));', memberInfo.varName, memberInfo.type, memberInfo.name)
                         else
                            writer:writeln('self.%s = self.AddComponent<%s, GObject>(obj.asCom.GetChildAt(%s));', memberInfo.varName, memberInfo.type, memberInfo.index)
                         end
                         writer:writeln('EntitySystemSingleton.Instance.Init(self.%s);',memberInfo.varName)
                    else
                        if getMemberByName then
                            writer:writeln('self.%s = (%s)obj.asCom.GetChild("%s");', memberInfo.varName, memberInfo.type, memberInfo.name)
                        else
                            writer:writeln('self.%s = (%s)obj.asCom.GetChildAt(%s);', memberInfo.varName, memberInfo.type, memberInfo.index)
                        end
                    end

               elseif memberInfo.group==1 then
                    if getMemberByName then
                        writer:writeln('self.%s = obj.asCom.GetController("%s");', memberInfo.varName, memberInfo.name)
                    else
                        writer:writeln('self.%s = obj.asCom.GetControllerAt(%s);', memberInfo.varName, memberInfo.index)
                    end
               else
                    if getMemberByName then
                        writer:writeln('self.%s = obj.asCom.GetTransition("%s");', memberInfo.varName, memberInfo.name)
                    else
                        writer:writeln('self.%s = obj.asCom.GetTransitionAt(%s);', memberInfo.varName, memberInfo.index)
                    end
               end
           end
           writer:endBlock()
        --   writer:writeln('[EntitySystem]')
        --   writer:writeln('private static void Destroy(this %s self)',classInfo.className)
         --  writer:startBlock()
         --  writer:endBlock()
           writer:endBlock()
           writer:endBlock()
           writer:save(exportCodePath..'/'..classInfo.className..'SystemGenerate.cs')
        


           writer:reset()
           writer:writeln('using FairyGUI;')
           writer:writeln()
           writer:writeln('namespace ET.Client')
           writer:startBlock()
           writer:writeln()
           writer:writeln('public partial class %s : UIViewComponent, IAwake<GObject>, IDestroy, IInit',classInfo.className)
           writer:startBlock()
           local memberCnt = members.Count
           for j=0,memberCnt-1 do
                local memberInfo = members[j]
                    local isclass = false
                    for z=0,classCnt-1 do
                        if classes[z].className == memberInfo.type then 
                            isclass = true
                        end
                    end
                    if isclass then 
                        writer:writeln('public EntityRef<%s> %s;',memberInfo.type,memberInfo.varName)
                    else
                        writer:writeln('public %s %s;',memberInfo.type,memberInfo.varName)
                    end
 

           end
           writer:endBlock()
           writer:endBlock()

    
           writer:save(exportCodePath2..'/'..classInfo.className..'Generate.cs')
    end

end

return genCode