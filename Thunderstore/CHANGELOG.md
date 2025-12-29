## 1.5.2

- Nevermind, CHEF can no longer use Roll until I fix the below issue properly.

## 1.5.1

- Fixed CHEF Roll hook causing player-controlled CHEF to sometimes release charge early.

## 1.5.0

- **General**
    - Implemented mod compatibility for Enforcer & Sonic.
    - Made the custom targeting for Huntress/Artificer/Seeker a little more performant. Maybe.
- **Engineer** - Thermal Harpoons will only be selected at max stocks.
- **Seeker** - Maximum target height for Unseen Hand reduced (100m -> 80m) so it can actually hit the target.
- **CHEF** - Can now use Roll! Always uncharged, though. Haven't figured out how to make it hold input.
- **Heretic** - Can now use Ruin!

## 1.4.0

- **General** - The GitHub's up.
- **MUL-T**
    - Fixed "Use Utility to ram close targets" not checking for LoS. It'll also ignore flying targets.
    - Implemented special behavior for Double Rebar + Retool.
    - Can now use Power Mode!
- **CHEF**
    - "Strafing while using primary" range increased. (30m -> 40m)
    - Glaze is active for 0.8s so it doesn't immediately get cancelled into Sear.
    - Can now use Yes, CHEF!

## 1.3.2

- Streets have taken me to a dimly lit alley and shot me in the back of the head. Not for comparing strings, though. It was for using async callbacks. Lesson learned.
    - This should make this mod play nicer with other mods in general.
- Fixed Captain freezing up when asked to drop his Supply Beacons.

## 1.3.1

- Streets are saying they will take me to a dimly lit alley and shoot me in the back of the head if I compare strings.

## 1.3.0

Alloyed Collective patch!
- **General**
    - Implemented behaviors for Operator & Drifter.
        - Credit where it's due, Drifter's vanilla behavior is surprisingly complex and features *18 skill drivers*. That said, I think there is still room for improvement :\)
    - Beefed up the Anti Corpse Shooting logic to include cases where the Survivor's target dies to something else entirely.
    - Goobo Jr. clones will, to some limited extent, mirror their owner's target.
        - Limited, because anything that causes them to refresh their target list (like being too far to attack or simply breaking line of sight) will undo that. I do not think this is a problem.
- **Commando** - "Strafing while shooting primary" range increased. (30m -> 40m)
- **Huntress**
    - Arrow Rain is now properly lined under the target, or on a wall behind the target if applicable.
    - Retreating range decreased. (30m -> 20m)

## 1.2.0

Just dropping what I have before Alloyed Collective drops.
- **Artificer**
    - Forgot about Nano-Spear piercing terrain, so now Artificer can wallbang targets with it.
    - Snapfreeze is now properly lined under the target.
    - Fixed her not fleeing if out of M1 stocks.
- **Captain**
    - If he has a leader, he will immediately attempt to place both Supply Beacons under himself.  I'm sure you can figure out some very interesting setups with that information. So interesting, in fact, that I will even grace you with a config to turn that off.
    - Now prioritizes his Secondary over his Utility to help line up the latter. Secondary can no longer be repeated multiple times in a row.
    - Starts strafing from further away. (25m -> 50m)
- **Seeker**
    - Unseen Hand is now properly lined under the target.
    - Will now use Meditate off cooldown.

## 1.1.0

- **General**
    - Successfully fixed that annoying thing where the AI will continue to target dead corpses for several seconds; on kill, they will re-target *very* quickly.
    - Implemented basic configs for Engineer's mobile turrets, in case you want to disable the changes or you're simply bored and want to give them cross-map reach.
    - Improved aim speed for Survivors across the board.
- **Huntress** - Standard Blink is no longer used while pathing to enemies due to it being *very* prone to overshooting, but may still be used mid-combat.
- **MUL-T** - Power Mode is disabled until I figure out simultaneous fire.
- **Artificer** - M2 is held *juuust* a little longer to prevent Arti from wasting it by cancelling into Snapfreeze.
- **Mercenary**
    - M1 is initiated slightly closer. (20m -> 15m)
    - Added a delay after performing his Utility so he has more time to orient his follow-ups correctly.
    - Now a little less Leeroy Jenkins-y and will occasionally strafe at range around a target.
- **Captain** - Compressed the attack ranges for his M1; the charge levels should be more appropriate for the situation.
- **Void Fiend** - Adjusted timings to reduce the likelyhood of firing an uncharged M2.
- **Seeker**
    - Fixed not being able to use Unseen Hand.
    - Sojourn is disabled until I figure out an intelligent way to cancel it in the behavior tree.

## 1.0.1

- Please work.

## 1.0.0

- Release.