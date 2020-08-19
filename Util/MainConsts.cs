﻿using System.Diagnostics;
using System.Reflection.Emit;

namespace PpmMain.Util
{
    public static class MainConsts
    {
        public const string CopyrightText = "© 2020 Biblica, Inc.";
        public const string ProgressBarInstalling = "Installing ...";
        public const string ProgressBarUninstalling = "Uninstalling ...";
        public const string ProgressBarUpdating = "Updating ...";
        public const string PostUpdateMessage = "Please restart Paratext for changes to take effect.";

        public const string LicenseFormAccept = "Accept";
        public const string LicenseFormCancel = "Cancel";
        public const string LicenseFormDismiss = "Dismiss";
        public const string LicenseFormTitle = "End User License Agreement";
        public const string LicenseFormDefaultText = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{\fonttbl{\f0\fnil\fcharset0 Times New Roman;}{\f1\fnil\fcharset0 Calibri;}}
{\colortbl ;\red255\green255\blue0;\red0\green0\blue255;}
{\*\generator Riched20 10.0.19041}\viewkind4\uc1 
\pard\sb100\sa100\b\f0\fs48\lang9 Plugin Software License Agreement\par

\pard\b0\f1\fs24 Last modified: August 3, 2020 \par
\par
This Plugin Software License Agreement (\ldblquote Agreement\rdblquote ) for use of the Software (as defined below) is entered into between Biblica, Inc., a Colorado non-profit corporation (\ldblquote Biblica\rdblquote ), and you. The Software is designed for use in connection with the Paratext software developed by United Bible Societies Association, Inc. (\ldblquote UBS\rdblquote ) and Summer Institute of Linguistics, Inc. (\ldblquote SIL\rdblquote ), but this Agreement is solely between Biblica and you; any third party (including UBS and SIL) to whose software or services the Software may allow connection is not a party to this Agreement. Separate license terms apply to your use of such third-party software and services. If you are accepting this Agreement on behalf of a corporation or other legal entity, you represent and warrant that you have the authority to bind that corporation or other legal entity to this Agreement, and, in such event, \ldblquote you\rdblquote  and \ldblquote your\rdblquote  will refer to that corporation or other legal entity.\par
\par

\pard\sb100\sa100\b\f0\fs36 1. Term and Termination.\par

\pard\b0\f1\fs24\par
This Agreement is effective on the date Licensee first installs or accesses the Software and will continue until (a) Biblica terminates this Agreement by sending written notice of termination to the other party (including via email) or (b) Licensee terminates the Agreement by ceasing to use the Software and complying with the obligations stated in Section 7 (Effects of Termination). Such termination shall take effect immediately.\par
\par

\pard\sb100\sa100\b\f0\fs36 2. Software and License.\par

\pard\b0\f1\fs24\par
(a)\~Under this Agreement, Licensee may use Biblica\rquote s plugin software offering(s) made available with or through this Agreement and any associated documentation (collectively referred to in this Agreement as the \ldblquote Software\rdblquote ) and for the limited uses described in this Agreement.\par
\par
(b) Subject to the terms of this Agreement, Biblica grants to Licensee a non-exclusive, non-sublicensable, non-transferable, limited license to copy and use the Software during the term of this Agreement, including (i) for Licensee\rquote s internal business purposes and (ii) to develop materials, so long as Licensee does not modify or distribute any component of the Software or include any component of the Software in the materials Licensee develops. Any materials Licensee develops using the Software may not violate (A) applicable laws, (B) Biblica\rquote s or any third party\rquote s rights, or (C) any Biblica policies of which Biblica notifies Licensee.\par
\highlight1\par
\highlight0 (c) Licensee shall not, and shall not allow End Users or third parties under its control to: (i) copy (except as expressly described in this Agreement), modify, create a derivative work of, reverse engineer, decompile, translate, disassemble, or otherwise attempt to extract any of the source code of the Software (except to the extent such restriction is expressly prohibited by applicable law); (ii) sublicense, transfer, or distribute any of the Software; (iii) sell, resell, or otherwise make the Software available to a third party as part of a commercial offering; or (iv) use the Software: (A) for High Risk Activities; or (B) in a manner that breaches, or facilitates the breach of, Export Control Laws.\par
\highlight1\par
\highlight0 (d) Third party components (which may include open source software) and other open source components of the Software may be subject to separate license agreements. To the limited extent a third party license or open source license expressly applies to a component of the Software, that license supersedes this Agreement and governs Licensee\rquote s use of that component. In addition, Licensee may be required to procure and utilize certain third party software to utilize the Software.\par
\highlight1\par
\highlight0 (e)\~The Software may send Biblica certain analytics data (for example, typesetting preview requests and Paratext username ). Biblica\rquote s collection and use of such data is governed by Biblica\rquote s Privacy Policy, available at {{\field{\*\fldinst{HYPERLINK https://www.biblica.com/privacy-policy/ }}{\fldrslt{https://www.biblica.com/privacy-policy/\ul0\cf0}}}}\f1\fs24 .\par
\par

\pard\sb100\sa100\b\f0\fs36 3. Ownership; Feedback.\par

\pard\b0\f1\fs24\par
Biblica and its suppliers and licensors shall own all right, title and interest to the Software. All rights not expressly granted in this Agreement are reserved by Biblica. Biblica may request that Licensee, at its option, provides Biblica with suggestions and feedback (\ldblquote Feedback\rdblquote ), which may include, without limitation, bug reports, documentation feedback, and verbal product feedback, or responses to short written questionnaires provided by Biblica from time to time. If Licensee provides Feedback related to the Software, then Biblica may use that information without obligation to Licensee, and Licensee assigns to Biblica all right, title, and interest in that Feedback. Where due to mandatory statutory law, such assignment is not permitted, Licensee herewith agrees to grant to Biblica a worldwide, perpetual (or for the maximum duration permitted by applicable law), irrevocable, royalty-free license to use and incorporate the Feedback into the Software. Licensee waives the right to be named as author of the Feedback.\par
\par

\pard\sb100\sa100\b\f0\fs36 4. No Support.\par

\pard\b0\f1\fs24\par
Biblica is not obligated under this Agreement to provide any technical support for the Software.\par
\par

\pard\sb100\sa100\b\f0\fs36 5. Indemnification.\par

\pard\b0\f1\fs24\par
Licensee agrees to hold harmless and indemnify Biblica, its employees, agents, and representatives, from and against any third party claim related to Licensee\rquote s use of the Software or other activities under this Agreement.\par
\par

\pard\sb100\sa100\b\f0\fs36 6. Disclaimers.\par

\pard\b0\f1\fs24\par
Notwithstanding anything to the contrary in this Agreement:\par
\par
a. The Software is provided \ldblquote as is\rdblquote  and to the fullest extent permitted by applicable law, Biblica does not make any warranties of any kind with respect to the Software, whether express, implied, statutory, or otherwise, including warranties of noninfringement or error-free or uninterrupted use of any component of the Software;\par
\par
b. Biblica shall not be liable under this Agreement or with respect to the Software for (i) lost revenues or indirect special, exemplary, or punitive damages; or (ii) any amounts in excess of $500 in total; and\par
\par
c. Licensee is responsible for protecting Licensee, Licensee\rquote s property and data, and others from any risks caused by the Software.\par
\par

\pard\sb100\sa100\b\f0\fs36 7. Effects of Termination.\par

\pard\highlight1\b0\f1\fs24\par
\highlight0 Upon termination of this Agreement, Licensee shall cease all use of the Software and delete all Software from its systems. Section 2(c) and Sections 3 (Ownership; Feedback) through 10 (Other Definitions) shall survive any termination of this Agreement.\par
\highlight1\par

\pard\sb100\sa100\highlight0\b\f0\fs36 8. Binding Arbitration.\par

\pard\highlight1\b0\f1\fs24\par
\highlight0 (a) ALL CLAIMS ARISING OUT OR RELATING TO THIS AGREEMENT OR ANY RELATED BIBLICA PRODUCTS OR SERVICES (INCLUDING ANY DISPUTE REGARDING THE INTERPRETATION OR PERFORMANCE OF THE AGREEMENT) (\ldblquote Dispute\rdblquote ) WILL BE GOVERNED BY THE LAWS OF THE STATE OF COLORADO, USA, EXCLUDING COLORADO\rquote S CONFLICTS OF LAWS RULES.\par
\par
(b) The parties shall try in good faith to settle any Dispute within 30 days after the Dispute arises. If the Dispute is not resolved within 30 days, it must be resolved by arbitration by the American Arbitration Association\rquote s International Centre for Dispute Resolution in accordance with its Expedited Commercial Rules in force as of the date of this Agreement (\ldblquote Rules\rdblquote ).\par
\par
(c) The parties shall mutually select one arbitrator. The arbitration shall be conducted in English in El Paso County, Colorado, USA.\par
\par
(d) Either party may apply to any competent court for injunctive relief necessary to protect its rights pending resolution of the arbitration. The arbitrator may order equitable or injunctive relief consistent with the remedies and limitations in this Agreement.\par
\par
(e) Subject to the confidentiality requirements in subsection (g), either party may petition any competent court to issue any order necessary to protect that party\rquote s rights or property; this petition shall not be considered a violation or waiver of this governing law and arbitration section and shall not affect the arbitrator\rquote s powers, including the power to review the judicial decision. The parties stipulate that the courts of El Paso County, Colorado, USA, are competent to grant any order under this Subsection (e).\par
\par
(f) The arbitral award shall be final and binding on the parties and its execution may be presented in any competent court, including any court with jurisdiction over either party or any of its property.\par
\par
(g) Any arbitration proceeding conducted in accordance with this Section shall be considered confidential information, including (i) the existence of, (ii) any information disclosed during, and (iii) any oral communications or documents related to the arbitration proceedings, and may not be disclosed to any third party. The parties may disclose the information described in this Subsection (g) to a competent court as may be necessary to file any order under Subsection (e) or execute any arbitral decision, but the parties must request that those judicial proceedings be conducted \i in camera\i0  (in private).\par
\par
(h) The parties shall pay the arbitrator\rquote s fees, the arbitrator\rquote s appointed experts\rquote  fees and expenses, and the arbitration center\rquote s administrative expenses in accordance with the Rules. In its final decision, the arbitrator shall determine the non-prevailing party\rquote s obligation to reimburse the amount paid in advance by the prevailing party for these fees.\par
\par
(i) Each party shall bear its own lawyers\rquote  and experts\rquote  fees and expenses, regardless of the arbitrator\rquote s final decision regarding the Dispute.\par
\par

\pard\sb100\sa100\b\f0\fs36 9. Miscellaneous.\par

\pard\b0\f1\fs24\par
Except for the rights expressly granted in this Agreement, each party retains all rights it would have independent of this Agreement. All legal notices must be in English, in writing (including email), and addressed to the other party\rquote s primary contact, which for Biblica is {\cf2\ul{\field{\*\fldinst{HYPERLINK ""mailto:legal @biblica.com""}}{\fldrslt{legal@biblica.com}}}}\f1\fs24 .\~Licensee may not assign this Agreement without the prior written consent of Biblica. Any amendment must be in writing and signed by both parties. This Agreement states all terms agreed between the parties and cancels and replaces all other agreements between the parties relating to its subject matter. Any use of \ldblquote including\rdblquote  in this Agreement means \ldblquote including but not limited to.\rdblquote  If any term (or part of a term) of this Agreement is invalid, illegal or unenforceable, the rest of this Agreement shall remain in effect.\par
\par

\pard\sb100\sa100\b\f0\fs36 10. Other Definitions.\par

\pard\b0\f1\fs24\par
\ldblquote End User\rdblquote means, if Licensee is an organization, an individual that Licensee permits to use the Software.\par
\par
\ldblquote Export Control Laws\rdblquote means all applicable export and re-export control laws and regulations, including (a) trade and economic sanctions maintained by the U.S.Treasury Department\rquote s Office of Foreign Assets Control; and(b) the International Traffic in Arms Regulations maintained by the U.S.Department of State, but excluding the Export Administration Regulations maintained by the U.S.Department of Commerce.\par
\par
\ldblquote High Risk Activities\rdblquote means activities where use or failure of the Software could lead to death, personal injury, or environmental damage, including operation of nuclear facilities, air traffic control, life support systems, or weaponry.\par
\highlight1\par

\pard\sb100\sa100\highlight0\b\f0\fs36 Previous Versions of Agreement\par

\pard\highlight1\b0\f1\fs24\par
\highlight0 None\par
\par
}";
    }
}
