EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)


VAR CollectPlasticQuestId = "CollectPlasticQuest"
VAR CollectPlasticQuestState = "REQUIREMENTS_NOT_MET"
VAR SaveTheOceanQuestId = "SaveTheOceanQuest"
VAR SaveTheOceanQuestState = "REQUIREMENTS_NOT_MET"
VAR GetToolboxQuestState = "REQUIREMENTS_NOT_MET"
VAR GetToolboxQuestId = "GetToolboxQuest"
VAR CollectWoodQuestId = "CollectWoodQuest"
VAR CollectWoodQuestState = "REQUIREMENTS_NOT_MET"
VAR ChestQuestId = "ChestQuest"
VAR ChestQuestState = "REQUIREMENTS_NOT_MET"

INCLUDE collect_plastic_start_npc.ink
INCLUDE collect_plastic_finish_npc.ink
INCLUDE save_the_ocean_start.ink
INCLUDE get_toolbox.ink
INCLUDE collect_wood.ink
INCLUDE get_chest.ink


= shopNotAvailable
Seems this shop is in need of repair
I should talk to that man about it
-> END

= cannotCollectTrash
I don't have what I need to collect this yet!
-> END

= trashTooHeavy
This item is to heavy, I need to unload at the net!
-> END

= player_collect_toolbox
This must be Scuttle's toolbox!
-> END

= player_collect_chest
This is the chest Sally was talking about!
-> END

= player_cannot_collect_toolbox
I don't really want to take a random toolbox back up with me.
-> END

= player_cannot_open_chest
This chest is way too heavy to lift. I need a key.
-> END

= player_already_open_chest
This chest is empty!
-> END

= sponge_flavor_text
I'm Ready I'm Ready
-> END





