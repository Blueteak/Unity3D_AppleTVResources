# Unity3D_AppleTVResources
Apple TV - Unity Build Resources

Versions 1.2 Release notes
- Added support for Game Center "Dashboard Artwork" 923px by 150px
This optional image, such as a game logo, appears at the top of your game’s dashboard. Transparency may be used to let the background show through the image. Dashboard artwork is not focusable. 

Versions 1.1 Release notes
- Switched from using resources to asset database so icons aren't included in the unity build
- Now supports flexible number of layers for image stacks
- Leaderboard icons added
- Added re-export menu item.
- Remove Plist Fixups as unity now does all of this

Note: When upgrading to this version you will have to reconnect your textures to the AppleTVIcons.asset as the format has changed.

Still to come - Adding App Transport Security Exceptions. As per this article. https://developer.apple.com/library/prerelease/ios/technotes/App-Transport-Security-Technote/
