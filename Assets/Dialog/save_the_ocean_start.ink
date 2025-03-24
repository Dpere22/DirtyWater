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
Notice the ocean health meter. Right now, the ocean is not healthy. 
When you collect garbage from the sea, you progress the health.
Please get the health to max, and then come and see me!
* [Ok!]
    ~StartQuest(SaveTheOceanQuestId)
    Great!
- -> END
= inProgress
Thank you for all the work you do. The ocean still needs healing!
-> END
= canFinish
Wow, the ocean is gorgeous.
Thank you for everything!
~FinishQuest(SaveTheOceanQuestId)
-> END
= finished
Thanks!
-> END