=== getToolbox ===
{ GetToolboxQuestState :
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
I shouldn't be saying this!
- -> END
= inProgress
If only I had my toolbox
-> END
= canFinish
~ CollectWoodQuestState = "IN_PROGRESS"
Wow, my toolbox
Now please go collect wood
~FinishQuest(GetToolboxQuestId)
~StartQuest(CollectWoodQuestId)
-> END
= finished
    -> collectWood
-> END