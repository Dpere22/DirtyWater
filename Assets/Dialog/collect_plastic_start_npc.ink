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
Oh, I see you are a researcher.
Welcome to our part of the ocean.
Sadly, the ocean is a mess from waste.
If you really want to help, will you bring me 5 plastic water bottles?
* [Yes]
    ~StartQuest(CollectPlasticQuestId)
    Great, bring me those bottles, and I will let you shop at my store!
* [No]
    Awwwwwwwww
- -> END
= inProgress
How is collecting the plastic going?
-> END
= canFinish
    ~ GetToolboxQuestState = "IN_PROGRESS"
    OH? You really do want to help!
    In that case, here is a bag, it will let you pick up large pieces of trash 
    Large trash is worth more materials
    Also, as promissed, you may now shop at my store!
    Now, go get my toolbox!
    ~FinishQuest(CollectPlasticQuestId)
    ~StartQuest(GetToolboxQuestId)
-> END
= finished
    -> getToolbox
-> END