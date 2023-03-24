# PingStatusBarIndicator
ChatGPT rebuilt an old project of mine... in 2 minutes...

This places a little color indicator representing internet quality (how many pings succeeded in last 10 seconds).

Green: >90%

Yellow: >50%

Red: <50%

---

I wrote a project like this myself many years ago. I've often found myself missing it, but not badly enough to actually put in the effort.

Then I remembered ChatGPT exists.

It didn't work the first time, but I just kept copy-pasting error messages into ChatGPT and it kept fixing them. 

(If it was able to compile C# by itself, it would have fixed them on its own ;)

Once it compiled, it worked the first time...

My prompt was:

> Hi! I once wrote a little statusbar indicator in C#. It would ping 8.8.8.8 once per second, and change the color of the statusbar icon depending on what % of the last few packets were dropped. Unfortunately I've lost the code. Could you help me rebuild the project? Please use the latest version of .NET with which you are familiar (and which would be appropriate for a Windows statusbar program).