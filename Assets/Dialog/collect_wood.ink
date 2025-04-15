=== collectWood ===
{ CollectWoodQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
//Not possible, but dialogue just in case
Lovely weather wood!
-> END
= canStart
I shouldn't be saying this wood!
- -> END
= inProgress
Please go get me the wood
-> END
= canFinish
~ChestQuestState = "IN_PROGRESS"
Nice you got the wood,
now go get the girl necklace
~FinishQuest(CollectWoodQuestId)
~StartQuest(ChestQuestId)
-> END
= finished
    -> getChest
-> END