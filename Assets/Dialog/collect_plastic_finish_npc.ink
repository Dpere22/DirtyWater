=== collectPlasticFinish ===
{ CollectPlasticQuestState:
    - "FINISHED": -> finished
    - else: -> default
}

= finished
Thank you! Now I can repair the shop
-> END

= default
If only I had some plastic to repair my shop
* [Just wait, I can help!]
    -> END
* {CollectPlasticQuestState == "CAN_FINISH" } [Here is some plastic!]
    ~FinishQuest(CollectPlasticQuestId)
    OH? This is perfect thank you, now I can repair my shop
-> END
