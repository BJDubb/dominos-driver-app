using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace DominosFloat
{
    public partial class FirstViewController : UIViewController
    {
        Dictionary<float, int> totals = new Dictionary<float, int>();
        Dictionary<UITextField, string> amountLUT = new Dictionary<UITextField, string>();
        
        public FirstViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UITapGestureRecognizer tap = new UITapGestureRecognizer(DismissKeyboard);
            tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(tap);
            InitializeInputs();
            AddPreviousNextButtonOnKeyboard(true, fiftyInput, twentyInput, tenInput, fiveInput, twoInput, oneInput, fiftyCentInput, twentyCentInput, tenCentInput, fiveCentInput);


            //UITextField twentyInput = new UITextField(new CoreGraphics.CGRect(124, 200, 100, 30));
            //twentyInput.BorderStyle = UITextBorderStyle.Line;
            //twentyInput.Layer.BorderColor = new CoreGraphics.CGColor(0, 0, 255);
            //twentyInput.Layer.BorderWidth = 1;
            //View.AddSubview(twentyInput);twentyInput
            // Perform any additional setup after loading the view, typically from a nib.
        }

        private void AddPreviousNextButtonOnKeyboard(bool previousNextable, params UITextField[] textFields)
        {
            for (int i = 0; i < textFields.Length; i++)
            {
                UITextField field = textFields[i];
                var toolbar = new UIToolbar(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 100));
                toolbar.BarStyle = UIBarStyle.Default;

                UIBarButtonItem[] items = new UIBarButtonItem[2];

                if (previousNextable)
                {
                    var previousButton = new UIBarButtonItem(UIImage.GetSystemImage("chevron.up"), UIBarButtonItemStyle.Plain, this, null);
                    if (field == textFields[0])
                    {
                        previousButton.Enabled = false;
                    }
                    else
                    {
                        previousButton.Enabled = true;
                        previousButton.Target = textFields[i - 1];
                        previousButton.Action = new ObjCRuntime.Selector("becomeFirstResponder");
                    }

                    var fixedSpace = UIBarButtonItem.GetFixedSpaceItem(8);

                    var nextButton = new UIBarButtonItem(UIImage.GetSystemImage("chevron.down"), UIBarButtonItemStyle.Plain, this, null);
                    if (field == textFields[textFields.Length - 1])
                    {
                        nextButton.Enabled = false;
                    }
                    else
                    {
                        nextButton.Enabled = true;
                        nextButton.Target = textFields[i + 1];
                        nextButton.Action = new ObjCRuntime.Selector("becomeFirstResponder");
                    }

                    var spacing = (toolbar.Bounds.Width / 2) - (previousButton.Width + fixedSpace.Width + nextButton.Width);
                    
                    var gapSpace = UIBarButtonItem.GetFixedSpaceItem(spacing);

                    var textButton = new UIBarButtonItem(amountLUT[field] ?? "T", UIBarButtonItemStyle.Plain, this, null);

                    items = new UIBarButtonItem[] { previousButton, fixedSpace, nextButton, gapSpace, textButton };
                }

                toolbar.SetItems(items, false);
                toolbar.SizeToFit();

                field.InputAccessoryView = toolbar;
            }
        }

        private void InitializeInputs()
        {
            amountLUT = new Dictionary<UITextField, string>
            {
                { fiftyInput, "$50" },
                { twentyInput, "$20" },
                { tenInput, "$10" },
                { fiveInput, "$5" },
                { twoInput, "$2" },
                { oneInput, "$1" },
                { fiftyCentInput, "50c" },
                { twentyCentInput, "20c" },
                { tenCentInput, "10c" },
                { fiveCentInput, "5c" },
            };

            fiftyInput.Text = "0";
            fiftyOutput.Text = "$0.00";
            twentyInput.Text = "0";
            twentyOutput.Text = "$0.00";
            tenInput.Text = "0";
            tenOutput.Text = "$0.00";
            fiveInput.Text = "0";
            fiveOutput.Text = "$0.00";
            twoInput.Text = "0";
            twoOutput.Text = "$0.00";
            oneInput.Text = "0";
            oneOutput.Text = "$0.00";

            fiftyCentInput.Text = "0";
            fiftyCentOutput.Text = "$0.00";
            twentyCentInput.Text = "0";
            twentyCentOutput.Text = "$0.00";
            tenCentInput.Text = "0";
            tenCentOutput.Text = "$0.00";
            fiveCentInput.Text = "0";
            fiveCentOutput.Text = "$0.00";

            totalNoteInput.Text = "0";
            totalCashInput.Text = "$0.00";

            fiftyOutput.ShouldBeginEditing = (field) => false;
            twentyOutput.ShouldBeginEditing = (field) => false;
            tenOutput.ShouldBeginEditing = (field) => false;
            fiveOutput.ShouldBeginEditing = (field) => false;
            twoOutput.ShouldBeginEditing = (field) => false;
            oneOutput.ShouldBeginEditing = (field) => false;
            fiftyCentOutput.ShouldBeginEditing = (field) => false;
            twentyCentOutput.ShouldBeginEditing = (field) => false;
            tenCentOutput.ShouldBeginEditing = (field) => false;
            fiveCentOutput.ShouldBeginEditing = (field) => false;

            totalNoteInput.ShouldBeginEditing = (field) => false;
            totalCashInput.ShouldBeginEditing = (field) => false;

            fiftyInput.EditingChanged += (s, e) => fiftyOutput.Text = CalculateAmount(fiftyInput.Text, 50);
            twentyInput.EditingChanged += (s, e) => twentyOutput.Text = CalculateAmount(twentyInput.Text, 20);
            tenInput.EditingChanged += (s, e) => tenOutput.Text = CalculateAmount(tenInput.Text, 10);
            fiveInput.EditingChanged += (s, e) => fiveOutput.Text = CalculateAmount(fiveInput.Text, 5);
            twoInput.EditingChanged += (s, e) => twoOutput.Text = CalculateAmount(twoInput.Text, 2);
            oneInput.EditingChanged += (s, e) => oneOutput.Text = CalculateAmount(oneInput.Text, 1);
            fiftyCentInput.EditingChanged += (s, e) => fiftyCentOutput.Text = CalculateAmount(fiftyCentInput.Text, 0.5f);
            twentyCentInput.EditingChanged += (s, e) => twentyCentOutput.Text = CalculateAmount(twentyCentInput.Text, 0.2f);
            tenCentInput.EditingChanged += (s, e) => tenCentOutput.Text = CalculateAmount(tenCentInput.Text, 0.1f);
            fiveCentInput.EditingChanged += (s, e) => fiveCentOutput.Text = CalculateAmount(fiveCentInput.Text, 0.05f);
        }

        private string CalculateAmount(string amount, float multiplier)
        {
            double result = 0.00d;
            if (int.TryParse(amount, out int value))
            {
                result = value * multiplier;
                UpdateTotals(multiplier, value);
            }

            return result.ToString("C2");
        }

        private void UpdateTotals(float key, int amount)
        {
            if (totals.ContainsKey(key))
            {
                totals[key] = amount;
            }
            else
            {
                totals.Add(key, amount);
            }
            int totalNotes = 0;
            double totalCash = 0;
            foreach (var note in totals)
            {
                totalNotes += note.Value;
                totalCash += (note.Key * note.Value);
            }
            totalNoteInput.Text = totalNotes.ToString();
            totalCashInput.Text = totalCash.ToString("C2");
        }

        private void DismissKeyboard()
        {
            View.EndEditing(true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
