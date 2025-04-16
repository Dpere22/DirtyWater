=== getChest ===
{ ChestQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
//Not possible, but dialogue just in case
Lovely weather!
-> END
= canStart
starting chest
~StartQuest(ChestQuestId)
- -> END
= inProgress
Mr. I wish I had my locket
-> END
= canFinish
~ SaveTheOceanQuestState = "IN_PROGRESS"
Wow, the chest
Now please go save the ocean
~FinishQuest(ChestQuestId)
~EnableQuest(SaveTheOceanQuestId)
-> END
= finished
    -> saveTheOceanStart
-> END