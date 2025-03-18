EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)


VAR CollectPlasticQuestId = "CollectPlasticQuest"
VAR CollectPlasticQuestState = "REQUIREMENTS_NOT_MET"

INCLUDE collect_plastic_start_npc.ink
INCLUDE collect_plastic_finish_npc.ink


