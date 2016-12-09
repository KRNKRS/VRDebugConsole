# VRDebugLog
これはVR開発上でデバッグの補助を行うアセットで、3D空間上にデバッグログの情報を表示します。<br>
現在、実装されている機能は以下。<br>
This asset is support to development that VR applications.<br>
VRDebugLog display in 3d space that information of DebugLog function.<br>
For the current function, please refer to the following.<br>

## Ver.0.5 機能<br>Function
<ul>
<li>リサイズ<br>Resize</li>
<li>ビルボード<br>Billborard</li>
<li>最小化<br>Minimize</li>
<li>ログのクリア<br>Log clear</li>
<li>タイプごとのログの表示/非表示切り替え<br>Change display by log type</li>
<li>項目選択によるスタックトレース表示<br>Display stacktrace of log</li>
<li>Viveコントローラによる限定的な操作<br>Limited operation by Vive controller</li>
</ul>
## 使い方<br>How to
<ol>
<li>インスペクターもしくはシーンビューにVRDebugプレハブをD&Dします<br>
D&D "VRDebug" prefab to inspector or scene view</li>
<li>終わり<br>
Complete</li>
</ol>
![D&D](https://cloud.githubusercontent.com/assets/3947216/21044584/0efda7ec-be40-11e6-889f-87ba8f4e2ae2.gif "D&D")<br>
<br>
好みのサイズと位置に調整して使用できます。<br>
You can adjust it to your preferred size and position.
![Resize and Billboard](https://cloud.githubusercontent.com/assets/3947216/21043606/02cb9222-be3b-11e6-9898-3014e3e5bdf6.gif "Resize and Billboard")<br>
<br>
HTC Viveを使用している場合は、EventSystemにアタッチされている「VRDebugInputModule」にVRDebugWindowの操作を行いたいコントローラを指定することで実行中にトリガーを引くことで操作を行うことが出来ます。<br>
If you are using HTC Vive, you can operate by pulling a trigger during execution by attach the controller you want to operate VRDebugWindow in "VRDebugInputModule" attached to EventSystem.
![Vive Controller Attach](https://cloud.githubusercontent.com/assets/3947216/21046523/75b92c88-be48-11e6-9bcc-a76f8e0ed32c.gif "Vive Controller Attach")
![Vive operate](https://cloud.githubusercontent.com/assets/3947216/21043849/4c8f64aa-be3c-11e6-80cf-610affdd40bf.gif "Vive operate")
