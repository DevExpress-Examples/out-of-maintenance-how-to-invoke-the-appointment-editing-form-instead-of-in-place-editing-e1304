<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128635435/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1304)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
<!-- default file list end -->
# How to invoke the Appointment Editing Form instead of in-place editing 


<p>The following example demonstrates how you can change the default action which is performed when the end-user presses the key in the Scheduler control. <br />
The in-place editor is invoked by default, enabling the user to modify the Subject field. This code intercepts the key press, and the appointment editing form is invoked instead of the in-place editor, allowing the user to edit all appointment fields, not the appointment's Subject alone.<br />
The <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument4107">Keyboard service</a> substitution technique is used to accomplish this task.</p>

<br/>


