=== getChest ===
{ ChestQuestState :
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
SHELLY: Hi!
SHELLY: Don’t tell grandpa but I took this key from his shed. I think it goes in a treasure chest! Grandpa never told me he was a pirate!
SHELLY: I think you’re super cool ms diver lady, and I want you to find the treasure! 
SCUTTLE: Shelly? What’s happening over here? 
SHELLY: NOTHING GRAMPA! I’m showing miss diver lady some shells!
SCUTTLE: Oh, ok. Just be careful for glass or anything sharp. Love you!
SHELLY: I love you too Grampa!
SHELLY: TO TREASURE!!! GOOD LUCK!!!
~StartQuest(ChestQuestId)
- -> END
= inProgress
SHELLY: The ocean is full of treasure!
-> END
= canFinish
SHELLY: You found it? OOOOH LEMME SEE! LEMME SEE!
SHELLY: WOOOOOWWWW ITS SO PRETTY.
SHELLY: I LOVE YOU MISS DIVER LADY!!
SCUTTLE: Hmm? Now what’s going on over here?
SHELLY: GRAMPA GRAMPA! She found treasure! Its SO PRETTY!
SCUTTLE: So it is. 
SCUTTLE: Shelly, I think I left some food out in the house. Could you be a dear and clean up for me? 
SHELLY: Awwww but I wanted to keep looking at the treasure…
SCUTTLE: You’ll get the chance kiddo, now go cleanup.
SCUTTLE: So, you found my chest. I was hoping that pendant wouldn’t find its way back… but yet again I never got rid of the key. Guess I can’t blame Shelly, or you for that matter.
SCUTTLE: This was a gift. It was from my wife, to her daughter. All that really remains of them is that little angel, and this necklace. 
SCUTTLE: Oh Marina… Portia… 
SCUTTLE: I won’t let you go again. I’m so sorry.
SHELLY: All clean Grampa! Lemme see the treasure!!
SCUTTLE: Deal’s a deal, kiddo. Just be careful with it, and give it back to me before you go inside. 
SHELLY: YAAAYYYYYY
SCUTTLE: You haven’t been here for long, but you’ve done a lot. Or at least you’ve done a lot to help this old man. Thank you. I really mean it.
~FinishQuest(ChestQuestId)
~EnableQuest(SaveTheOceanQuestId)
-> END
= finished
    -> saveTheOceanStart
-> END