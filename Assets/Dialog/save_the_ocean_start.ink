=== saveTheOceanStart ===
{ SaveTheOceanQuestState :
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
I shouldn't be saying this ocean!
- -> END
= inProgress
The ocean still needs healing!
-> END
= canFinish
Wow, the ocean is gorgeous.
Thank you for everything!
~FinishQuest(SaveTheOceanQuestId)
-> END
= finished
Thanks!
-> END