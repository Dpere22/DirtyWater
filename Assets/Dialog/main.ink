EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)


VAR CollectPlasticQuestId = "CollectPlasticQuest"
VAR CollectPlasticQuestState = "REQUIREMENTS_NOT_MET"

INCLUDE collect_plastic_start_npc.ink
INCLUDE collect_plastic_finish_npc.ink


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