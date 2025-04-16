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
SCUTTLE: Another day, another chance to see these waters shine! The shore hasn’t looked this clean in years!
~StartQuest(SaveTheOceanQuestId)
- -> END
= inProgress
SCUTTLE: You’re really making a difference. Keep at it! 
SHELLY: I believe in you miss diver lady!
-> END
= canFinish
SCUTTLE: Look at this place. This island is only a small piece of the big picture, this is just
SCUTTLE: This is incredible. 
SCUTTLE: Good work kid. That’s all this old man can say. Look around, let that do the talking.
SCUTTLE: You gave my sunshine a chance to live and grow up on the shore. Just like her mom, and her grandma. 
SCUTTLE: All I can say is thanks.
~FinishQuest(SaveTheOceanQuestId)
-> END
= finished
Thanks!
-> END