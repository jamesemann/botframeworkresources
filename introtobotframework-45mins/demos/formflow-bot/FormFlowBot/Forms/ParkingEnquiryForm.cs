using System;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace FormFlowBot.Forms
{
    [Serializable]
    public class ParkingEnquiryForm
    {
        //[Prompt("Please provide the machine ID, there should be a white label on the parking machine.  If you don't know this, then your street address can help us locate it. ")]
        public string MachineIdOrPostcode { get; set; }
        //[Prompt("Are you reporting a fault with the machine?")]
        public bool IsFault { get; set; }
        //[Prompt("Can you please describe your issue so with enough detail that we can resolve it as quickly as possible.")]
        public string Problem { get; set; }

        public static IForm<ParkingEnquiryForm> BuildForm()
        {
            var formbuilder = new FormBuilder<ParkingEnquiryForm>()
                //.Field(nameof(IsFault))
                //.Field(nameof(MachineIdOrPostcode))
                //.Confirm(
                //    async state =>
                //    {
                //        return
                //            new PromptAttribute(
                //                $"![img](https://maps.google.com/maps/api/staticmap?center={state.MachineIdOrPostcode}&zoom=15&size=400x400&maptype=roadmap&markers=color:ORANGE|label:A|{state.MachineIdOrPostcode}&sensor=false) \r\n\r\n\r\nIs the parking meter here?");
                //    })
                //.Field(nameof(Problem), state => { return state.IsFault; })
                //.OnCompletion(async (context, state) =>
                //{
                //    if (state.IsFault)
                //        await context.PostAsync(
                //            "Thank you for reporting the fault, we will have someone look at it immediately and contact you back on this channel");

                //    else
                //        await context.PostAsync(
                //            $"You can see information for your location ({state.MachineIdOrPostcode}) at https://parking.gov/info.pdf");
                //})
                .Build();

            return formbuilder;
        }
    }
}