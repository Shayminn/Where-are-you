using UnityEngine;

public static class Lines {
    public static string[] PickUpLines = new string[] {
        "Are you a pokemon? Because I'd hit you with my balls.",
        "Mario is red, Sonic is blue, grab a controller and be my player 2.",
        "If earth didn't have gravity, I would still fall for you.",
        "I'm not a photographer but I can picture you and me together.",
        "Are you from Tennessee? Because you¡¯re the only ten I see!",
        "There must be something wrong with my eyes, I can¡¯t take them off you.",
        "I¡¯d say God Bless you, but it looks like he already did.",
        "Are you a parking ticket? Because you¡¯ve got \"FINE\" written all over you.",
        "Hey, do you have a name, or can I just call you mine?",
        "Wanna know what's on the MENU? Me N u.",
        "If you stood in front of a mirror and held up 11 roses, you would see 12 of the most beautiful things in the world.",
        "Something is wrong with my cell phone... your numbers not in it."
    };

    public static string[] RejectionLines = new string[] {
        "Oh No! The Pokemon broke free!",
        "Sorry, but your princess is perhaps in another castle.",
        "Good luck because I'm not catching you.",
        "Thank God you're not a photographer, your aesthetic sense is really off.",
        "No, I'm from Alabama.",
        "Looks like your brain isn't working either, because you obviously can't tell I'm not interested.",
        "Well God definitely didn't bless you as well that's for sure...",
        "I'll do the honors of throwing myself out the window.",
        "Actually, I prefer to be called not interested.",
        "I'd rather go hungry.",
        "If you stood in front of a mirror besides a pile of shit, you'd see two piles of shit.",
        "Welp, looks like it will stay broken."
    };

    public static string[] GenerateRandomLines() {
        int random = Random.Range(0, PickUpLines.Length);

        return new string[2] { PickUpLines[random], RejectionLines[random] };
    }
}
