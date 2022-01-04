// See https://aka.ms/new-console-template for more information

using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

const string queueUrl = "https://sqs.us-east-2.amazonaws.com/697150559871/MyFirstQueue";
var sqsClient =
    new AmazonSQSClient(new BasicAWSCredentials("AKIA2EULJKZ7QSOYAYVN", "MQiHTcOOlQ93DuEiOatyCavRx+enN5/i4D/QekoK"), RegionEndpoint.USEast2);
while (true)
{
    var receiveMessageAsync =
        await sqsClient.ReceiveMessageAsync(
            new ReceiveMessageRequest(queueUrl));
    if (receiveMessageAsync.Messages.Count != 0)
    {
        foreach (var message in receiveMessageAsync.Messages)
        {
            Console.WriteLine($"Message Received: {message.Body}");
            await sqsClient.DeleteMessageAsync(new DeleteMessageRequest(queueUrl, message.ReceiptHandle));
        }
    }


    Thread.Sleep(1000);
}