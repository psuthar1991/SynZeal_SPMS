var Projects = {
    
    //Get Explaination from status

    GetExplaination: function (status) {
        var statusId = 0;
        if (status !== "") {
            statusId = parseInt(status);
        }

        var explaination = '';
        if (statusId === 1) {
            explaination = 'Thank you for your valuable order and we confirm the safe receipt of the same. We have initiated the process to synthesize this product and we will try to complete the synthesis as per the estimated completion date. ';
        }
        if (statusId === 2) {
            explaination = 'We have initiated the process to synthesize this product and we will try to complete the synthesis as per estimated completion date. ';
        }
        if (statusId === 3) {
            explaination = 'We have initiated the process to synthesize this product and we will try to complete the synthesis as per estimated completion date. ';
        }
        if (statusId === 4) {
            explaination = 'We have ordered the raw material and we will try to complete the synthesis as per estimated completion date. ';
        }
        if (statusId === 5) {
            explaination = 'We are facing delay in receiving the raw material and once we received we will complete the synthesis as per estimated completion date. ';
        }
        if (statusId === 6) {
            explaination = "We are facing challenges to get the raw material and due to which we can't predict the completion timeline.Meanwhile we request you to source from instock vendor and we will accept order cancellation.However, once we receive the raw material we will update the same.";
        }
        if (statusId === 7) {
            explaination = "Due to unavailability or delay in receiving the raw material, we are doing in house raw material synthesis. So, we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 8) {
            explaination = "Synthesis is in progress and we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 9) {
            explaination = 'We are facing synthetic challenges and we are trying to resolve as per estimated completion date. ';
        }
        if (statusId === 10) {
            explaination = "We are facing synthetic challenges and due to no positive outcome, we are not able to  predict the completion time. We request you to source from instock vendor for immediate use. Meanwhile, we will keep working and once synthesized we will update the same. ";
        }
        if (statusId === 11) {
            explaination = 'Final step purification is in progress and we will try to complete the purification as per estimated completion date. ';
        }
        if (statusId === 12) {
            explaination = "We have generated the crude and currently the batch is under prep hplc purification. We will complete the purification as per estimated completion date. ";
        }
        if (statusId === 13) {
            explaination = "We are facing challenges in purification and we are trying to resolve in stipulated time period. ";
        }
        if (statusId === 14) {
            explaination = "We are facing purification challenges and due to no positive result, we can't predict the completion timeline. We request you to source from instock vendor for immediate use. Meanwhile, we will keep working and once synthesized we will update the same. ";
        }
        if (statusId === 15) {
            explaination = "We are facing intricate issues in the synthesis and proposed ROS outcome is not positive. We are re-visiting other routes possible. We will keep you posted on the progress at regular interval.";
        }
        if (statusId === 16) {
            explaination = "We are facing synthetic challenges and due to no positive result, we can't predict the completion timeline. We request you to source from instock vendor for immediate use and we will accept order cancellation.  ";
        }
        if (statusId === 17) {
            explaination = "We are facing challenges in purification and due to no positive results we can’t predict the completion timeline. We request you to source from instock vendor for immediate use and we will accept order cancellation.  ";
        }
        if (statusId === 18) {
            explaination = "Analytical data recording is in progress and we will try to complete the analysis as per estimated completion date. ";
        }
        if (statusId === 19) {
            explaination = "We have this product in (_) form against (_)and if this serve your purpose then please let us know enable us to dispatch the same.";
        }
        if (statusId === 20) {
            explaination = "We have initiated the process to synthesize this product and we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 21) {
            explaination = "We have initiated the process to synthesize this product and we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 22) {
            explaination = "We have initiated the process to synthesize this product and we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 23) {
            explaination = "We have ordered the raw material and we will complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 24) {
            explaination = "Synthesis is in progress and we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 25) {
            explaination = "Synthesis is in progress and we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 26) {
            explaination = "Final step purification is in progress and we will try to complete the purification as per estimated completion date. ";
        }
        if (statusId === 27) {
            explaination = "We are facing challenges in purification and we are trying to resolve as per estimated completion date. ";
        }
        if (statusId === 28) {
            explaination = "Analytical data recording is in progress and we will try to complete the analysis as per estimated completion date. ";
        }
        if (statusId === 29) {
            explaination = "Analytical data recording is in progress and we will try to complete the analysis as per estimated completion date. ";
        }
        if (statusId === 30) {
            explaination = "Instock batch re-analysis is in progress and we will try to complete the analysis as per estimated completion date. ";
        }
        if (statusId === 31) {
            explaination = "Batch data is under QC approval and we will submit the batch as per estimated completion date.";
        }
        if (statusId === 32) {
            explaination = "Batch data has been approved by QC and we will submit the batch as per estimated completion date. ";
        }
        if (statusId === 33) {
            explaination = "QC has raised query in submitted batch and we will resolve and submit as per estimated completion date. ";
        }
        if (statusId === 34) {
            explaination = "QC has rejected the batch. So we are doing synthesis again and we will try to complete the synthesis as per estimated completion date. ";
        }
        if (statusId === 35) {
            explaination = "QC has rejected the current batch, so we are doing re-treatment and we will try to complete the process as per estimated completion date. ";
        }
        if (statusId === 36) {
            explaination = "QC has rejected the current batch, so we are doing re-synthesis and we will try to complete the process as per estimated completion date. ";
        }
        if (statusId === 37) {
            explaination = "QC has rejected the current batch, so we are doing re-purification and we will try to complete the process as per estimated completion date. ";
        }
        if (statusId === 38) {
            explaination = "Product is available now and processed for dispatch. ";
        }
        if (statusId === 39) {
            explaination = "We have shared the data for approval and we will dispatch the product after your confirmation.";
        }
        if (statusId === 40) {
            explaination = "We have received your query and we will resolve after then we will share the revised data for approval. ";
        }
        if (statusId === 41) {
            explaination = "Data has been approved and we will dispatch as per estimated completion date.  ";
        }
        if (statusId === 42) {
            explaination = "We have shared the Invoice for approval and we will dispatch the product after your confirmation.";
        }
        if (statusId === 43) {
            explaination = 'Our Account team will communicate further regarding dispatch.';
        }
        if (statusId === 44) {
            explaination = 'Our Account team will communicate further regarding dispatch.';
        }
        if (statusId === 45) {
            explaination = 'Our Account team will communicate further regarding dispatch.';
        }
        if (statusId === 46) {
            explaination = 'Product is cancelled/on hold as per instruction.';
        }

        return explaination;
    }
};