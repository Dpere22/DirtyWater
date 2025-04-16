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
SCUTTLE: The shop is ready for ya, kid! Now you just gotta get the proper materials to get started! Go out and grab 20 wood.
~StartQuest(CollectWoodQuestId)
- -> END
= inProgress
SCUTTLE: I “wood’ help you, but we need more before we can upgrade anything… I’m sorry, but puns make me happy.
-> END
= canFinish
~ChestQuestState = "IN_PROGRESS"
SCUTTLE: Good work! Now let's fix up this junk!
~FinishQuest(CollectWoodQuestId)
~EnableQuest(ChestQuestId)
-> END
= finished
    -> getChest
-> END