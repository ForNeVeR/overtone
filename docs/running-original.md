Running the Original Game
=========================
These days, it's not so easy to run a game from 1998. Here're the steps I've
followed to run the game on my Windows 10 machine (only some parts of this
instruction are specific to a Windows 10 host though).

Installing VirtualBox
---------------------
Yeah, the game doesn't run natively on modern OS (or I wasn't so lucky, at
least), so we'll use VirtualBox to run the game. I've tried Hyper-V, too, but
the game wasn't working for me on Hyper-V.

[Download VirtualBox][virtualbox.download] for your host OS and install it. I
was using VirtualBox 6.1.14 for this experiment, and, notably, it wasn't
conflicting with Hyper-V instance I was running at the same time.

Obtaining the Windows XP Image
------------------------------
Windows XP is the latest OS I've found the game to work with, so we'll use it,
and will even use the image legally available from the vendor.

Follow [this guide][stackoverflow.windows-xp] to extract the image (up to and
including the step 4). You'll now have a file `VirtualXP.VHD` on your disk
somewhere.

Please note that, as of writing this, the links to the Windows XP Mode in this answer are outdated. A download is still available [from the Internet archive][archive.windows-xp-mode].

Setting Up the Virtual Machine
------------------------------
1. Create a virtual machine in VirtualBox, and add `VirtualXP.VHD` as a disk
   image to it. I've also added 1024 MiB RAM, though the game doesn't require as
   much.
2. When VirtualBox will allow to use mouse integration, disable it (mouse integration doesn't worked well for me).
3. Then, install Windows XP as you usually would (no customization is expected at this stage).
4. At some point the machine may show you a black screen with only an
   (immovable) mouse cursor in it. At this stage, just restart the VM: it will
   be fine. I've found that it's the usual state of the OS during restart, just
   be ready for it.
5. After restart, disable automatic update, and allow OS to install any hardware
   it was able to install without Internet access (it is not required for the
   game at all). Allow the OS to correct your resolution if it offers to.
6. Ensure sound is working on your VM (via the tray icon for sound devices).
7. Insert the Guest Additions CD Image into the VM, and install everything
   available from it (the main thing being the graphics driver).

   You'll be required to confirm driver installation several times during the
   installation. It may take up to 15 minutes (probably due to Windows trying to
   search drivers on the malfunctioning Windows Update site).

   You will be required to reboot after the Guest Additions installation.

One notable problem I've found while trying to create snapshots of this VM was
fixed by [this forum post][virtualbox.snapshot-troubleshooting].

Installing the Game
-------------------
You'll need to download the game ISO file to install. I've used the "Alternative
ISO Version" from [My Abandonware][myabandonware.the-tone-rebellion]. Download
the archive and extract `TONE116E.iso` file from it somewhere to your disk.

Insert it onto your virtual machine, and the game will offer you the welcome
screen. Press the **Install the Tone Rebellion** button, we don't need anything
else here.

You're now all set: either press the **Single Player** button on the welcome
screen, or start the game from the Windows' **Start** menu.

[archive.windows-xp-mode]: https://archive.org/details/windows-xp-mode_20200907
[myabandonware.the-tone-rebellion]: https://www.myabandonware.com/game/the-tone-rebellion-cjc
[stackoverflow.windows-xp]: https://superuser.com/a/1230653/286768
[virtualbox.download]: https://www.virtualbox.org/wiki/Downloads
[virtualbox.snapshot-troubleshooting]: https://forums.virtualbox.org/viewtopic.php?f=6&t=79896
