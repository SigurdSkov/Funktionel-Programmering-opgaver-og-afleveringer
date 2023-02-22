module WavePacker

open System
open System.IO
open System.Text

// TODO 7 write data to stream
// http://soundfile.sapp.org/doc/WaveFormat/
// subchuncksize 16
// audioformat: 1s
// num channels: 1s
// sample rate: 44100
// byte rate: sample rate *16/8
// block origin: 2s
// bits per sample: 16s
let pack (d: int16[]) =
    let stream = new MemoryStream()
    let writer = new BinaryWriter(stream, Encoding.ASCII)
    let dataLength = (Array.length d*2)
    
    let RIFFChunkID = "RIFF" //0x52494646
    let subchunk1Size = 16 //Det er standarden? PCM
    let RIFFFormatString = "WAVE" //0x57415645
    let audioFormat = 1s //Linear Quantization
    let numChannels = 1s //stereo
    let sampleRate = int32 44100 //Angivet
    let bitsPerSample = 16s //Kommer egentlig senere i processen. Bits er enten 8, 16, 32, 64... Aner ikke hvad det faktisk er
    let byteRate = sampleRate * int32 numChannels * ((int32 bitsPerSample)/8) //byte rate: sample rate *16/8 Det jeg har skrevet er også korrekt
    let blockAlign = numChannels * bitsPerSample/8s //Er dette block origin? Det er i hvert fald 2s
    // data
    let subchunk2IDString = "data" //0x64617461
    let subchunk2Size = Array.length d * (int32 numChannels * (int32 bitsPerSample / 8)) // TODO up
    let RIFFChunkSize = 4 + (8 + subchunk1Size) + (8 + subchunk2Size)
    let byteArray = Array.map byte d //map d

    //Writes go here
    writer.Write (Encoding.ASCII.GetBytes(RIFFChunkID))
    writer.Write (int32 RIFFChunkSize)
    writer.Write (Encoding.ASCII.GetBytes(RIFFFormatString))
    writer.Write (Encoding.ASCII.GetBytes("fmt "))
    writer.Write (int32 subchunk1Size)
    writer.Write audioFormat
    writer.Write numChannels
    writer.Write (int32 sampleRate)
    writer.Write byteRate   
    writer.Write blockAlign
    writer.Write bitsPerSample
    writer.Write (Encoding.ASCII.GetBytes(subchunk2IDString))
    writer.Write subchunk2Size
    writer.Write byteArray

    stream

//-RIFF		        01-03	4
//-CHUNKSIZE	    04-07	4
//-FORMAT		    08-11	4
//-CHUNK1ID	        12-15	4
//-CHUNK1SIZE	    16-19  	4
//-AUDIOFORMAT	    20-21   2
//-NUMCHANNELS	    22-23  	2
//-SAMPLERATE	    24-27	4
//-BYTERATE	        28-31	4
//-BLOCKALIGN	    32-33  	2
//-BITSPERSAMPLE    34-35  	2
//-CHUNK2ID	        36-39	4
//-CHUNK2SIZE	    39-42	4
//data

//Previous data translations
//Recursive
//let rec elementWriter (writer: BinaryWriter) (data: int16[]) (elementCount: int32) =
//    writer.Write data[elementCount]
//    if data.Length = elementCount then 0
//    else elementWriter writer data elementCount+1

//let thirtyTwoArray = Array.map int32 d
//let charArray = Array.map char d
//writer.Write byteArray
//writer.Write thirtyTwoArray
//writer.Write charArray
//writer.Write Array.ForEach d
//let result = elementWriter writer d 0

    
let write filename (ms: MemoryStream) =
    use fs = new FileStream(Path.Combine(__SOURCE_DIRECTORY__, filename), FileMode.Create) // use IDisposible
    ms.WriteTo(fs)
    fs.Close()

