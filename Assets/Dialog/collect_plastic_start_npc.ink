=== collectPlasticStart ===
{ CollectPlasticQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}
= requirementsNotMet
//Not possible, but dialogue just in case
Come back once you've leveled up a bit more.
-> END
= canStart
Will you collect 5 pieces of plastic and bring them to me?
* [Yes]
    ~StartQuest(CollectPlasticQuestId)
    Great!
* [No]
    Awwwwwwwww
- -> END
= inProgress
How is collecting the plastic going?
-> END
= canFinish
    ~ SaveTheOceanQuestState = "IN_PROGRESS"
    OH? This is perfect thank you, now I can repair my shop
    Here is net, it will let you pick up large bags of trash!
    Notice the ocean health meter. Right now, the ocean is not healthy. 
    When you collect garbage from the sea, you progress the health.
    Please get the health to max, and then come and see me!
    ~FinishQuest(CollectPlasticQuestId)
    ~StartQuest(SaveTheOceanQuestId)
-> END
= finished
    -> saveTheOceanStart
-> END