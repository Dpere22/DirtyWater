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
Scuttle: Hey kid! Help an old man out, would ya?
Scuttle: See these waters? Bit too dirty for Shelly to be swimming in, I reckon.
Scuttle: If you can pick up 5 pieces of plastic, I'd feel more comfortable with her getting closer to the water
Shelly: Waves! Waves!
Scuttle: Not yet, kiddo.
Scuttle: Anyways, I'd really appreaciate it. Be seeing ya.
~StartQuest(CollectPlasticQuestId)
- -> END
= inProgress
Ha! You've got a ways to go, kid. Keep pick'n up plastic bottles.
-> END
= canFinish
    Scuttle: Well I'll be. I'd say the water is already looking a bit better! Still a mess, but I can rest a bit easier now. Thanks.
    Shelly: Grandpa! I wanna go in the water nowwwwww
    Scuttle: Alright kiddo, let's get you in those waves!
    ~FinishQuest(CollectPlasticQuestId)
    ~EnableQuest(GetToolboxQuestId)
-> END
= finished
    -> getToolbox
-> END