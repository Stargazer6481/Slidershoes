<p align="center">
  <img src="Slidershoes_wide.png" alt="Slidershoes Banner" width="600"/>
</p>

# Slidershoes

**An open source SteamVR "treadmill" using a mouse attached to your shoe with a slippery surface.**  
Turn any space into an omni-directional locomotion platform without spending thousands of dollars.

---

## ✨ Features
- Works as a custom **SteamVR driver** (shows up in SteamVR status).
- Tracks foot movement using a standard optical mouse.
- Emulates an omni-directional treadmill at a fraction of the cost.
- Open source, easy to hack and extend.

---

## 🛠 Installation

1. **Download / Build**
   - Clone this repo:
     ```bash
     git clone https://github.com/YOURNAME/Slidershoes.git
     ```
   - Open the project in **Visual Studio 2022** (x64).
   - Build in **Release | x64** to produce `driver_slidershoes.dll`.

2. **Install Driver**
   - Copy the built files into your SteamVR drivers folder:
     ```
     C:\Program Files (x86)\Steam\steamapps\common\SteamVR\drivers\slidershoes\
     ```
     Inside you should have:
     ```
     /bin/win64/driver_slidershoes.dll
     /resources/icons/shoe_status_on.png
     /resources/icons/shoe_status_off.png
     /driver.vrdrivermanifest
     ```

3. **Register with SteamVR**
   - Restart SteamVR.
   - In `steamvr.vrsettings`, ensure your driver is listed under `"steamvr" -> "activateMultipleDrivers": true`.

4. **Test**
   - Put your slippery shoes on, attach a mouse, and check the SteamVR status window.  
   - You should see your **Slidershoes tracker** appear.

---

## 📂 Repository Structure
Slidershoes/
├─ SlidershoesDriver/ → Source code for the SteamVR driver
├─ resources/icons/ → SteamVR status icons
├─ docs/title.png → Banner image
├─ driver.vrdrivermanifest → SteamVR driver manifest
└─ README.md

---

## 🖼 Icons
Custom icons are provided for driver status:
- `shoe_status_on`
- `shoe_status_off`
- `shoe_status_standby`
- `shoe_status_standby_error`
- `shoe_status_on_error`

---

## 🚀 Roadmap
- Add configuration UI (toggle mouse mode, sensitivity, etc.)
- Support multiple mice (one per foot).
- Community feedback and feature requests.

---

## 🤝 Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you’d like to add.

---

## 📜 License
MIT License — feel free to modify, share, and build on Slidershoes.

---

<p align="center">
  <sub>Made with ❤️ for VR hackers everywhere.</sub>
</p>
