<p align="center">
  <img src="Slidershoes_wide.png" alt="Slidershoes Banner" width="600"/>
</p>

# Slidershoes

**An open-source SteamVR "treadmill" using a mouse attached to your shoe with a slippery surface.**  
Turn any space into an omni-directional locomotion platform without spending thousands of dollars.

---

## Features

- Converts simple movements into virtual joystick input for VR games.
- Lock the mouse to center while using a keyboard key to unlock.
- Adjustable sensitivity slider for your preferred speed.
- Real-time joystick visualizer with animated feedback.
- Fancy dark-themed UI with your accent color (#f78036) and rounded controls.
- Stops safely with F11 or the stop button.
- Open source and customizable for your own setup.

---

## How I Built My Prototype

1. **Shoes & Slippery Surface**  
   - I used an old pair of extra shoes and added a slippery material to the bottom.  
   - Almost anything slick works—plastic, Tupperware pieces, or smooth fabric.

2. **Staying in Place**  
   - To simulate walking without moving in real life, you need some way to hold yourself in place.  
   - Example: attach a light backpack to the ceiling with a little tension so it gently holds you back but doesn’t restrict movement too much.  
   - (I haven’t done the backpack part yet, but testing with someone holding me worked well.)

3. **Mouse on the Shoe**  
   - Mount a wireless mouse to the tip of one shoe.  
   - You may need to take it apart to make it smaller and more comfortable.  
   - The mouse acts as a motion sensor for walking in place.

4. **Testing**  
   - You can start by having someone gently hold you in place while walking in the slippery shoes.  
   - Adjust sensitivity in the Slidershoes app to get smooth movement in VR.

---

## Setup & Usage

1. Build the `Slidershoes` project in Visual Studio.  
2. Run `Slidershoes.exe`.  
3. Click **START** to connect the virtual controller.  
4. Adjust the **sensitivity slider** to your liking.  
5. Lock the mouse to center using the checkbox and hold **Space** to temporarily unlock.  
6. Stop safely with **STOP** or **F11**.  

---

## Notes

- Works best with a wireless mouse mounted securely to the shoe.  
- Ensure you have enough space to walk safely.  
- Slidershoes is experimental—use caution while testing.

---

## Future Plans

- Implement ceiling backpack mount for consistent resistance.  
- Explore multi-shoe/multi-mouse setups for better realism.  
- Add VR overlay showing foot positions and step intensity.

---

## License

MIT License. Feel free to modify, fork, and improve!

---

<p align="center">
  <sub>Made with love for cheap VR players everywhere.</sub>
</p>
