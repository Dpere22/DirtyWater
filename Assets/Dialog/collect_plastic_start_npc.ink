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
Oh? You collected that plastic! Give them to the shop and they will give you something special!
-> END
= finished
Thanks for collecting that plastic!
-> END