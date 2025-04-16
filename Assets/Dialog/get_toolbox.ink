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
SCUTTLE: Hey kid, I just wanted to say thanks again for your help. You made Shelly’s whole week.
SCUTTLE: So listen, I was thinking I could return the favor. I want to improve my shop, so you can get some better equipment for your dives.
SCUTTLE: See, the only issue is that my lucky toolbox was in my pocket when I went to play with Shelly in the waves…
SCUTTLE: Now, I’m not usually one to believe in superstition, but I need that toolbox back. Shouldn’t have gone too far by now.
SCUTTLE: Think you can help me out?
~StartQuest(GetToolboxQuestId)
- -> END
= inProgress
If only I had my toolbox
-> END
= canFinish
SCUTTLE: There she is! What a beaut. You know, my wife gave me this hammer for our anniversary 30 something years ago…
SCUTTLE: I lied to you. This heavy thing isn’t really my lucky toolbox… but this hammer is special to me. Thank you for your help.
~FinishQuest(GetToolboxQuestId)
~EnableQuest(CollectWoodQuestId)
-> END
= finished
    -> collectWood
-> END